﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Fido2NetLib;
using Fido2NetLib.Development;
using Fido2NetLib.Objects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace cube.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebAuthTestController : ControllerBase
    {
        private static readonly DevelopmentInMemoryStore DemoStorage = new DevelopmentInMemoryStore();

        private Fido2 _lib;
        private IMetadataService _mds;
        private string _origin;

        public WebAuthTestController(IConfiguration config)
        {
            // Sample bogus key from https://fidoalliance.org/metadata/
            var invalidToken = "6d6b44d78b09fed0c5559e34c71db291d0d322d4d4de0000";
            _origin = config["https://localhost:44329"];
            _mds = MDSMetadata.ConformanceInstance(invalidToken, config["fido2:MDSCacheDirPath"], _origin);
            if (false == _mds.IsInitialized())
                _mds.Initialize().Wait();

            _lib = new Fido2(new Fido2Configuration()
            {
                ServerDomain = config["localhost"],
                ServerName = "Fido2 test",
                Origin = _origin,
                MetadataService = _mds
            });
        }

        [HttpPost]
        [Route("/attestation/options")]
        public IActionResult MakeCredentialOptionsTest([FromBody] TEST_MakeCredentialParams opts)
        {

            var attType = opts.Attestation;

            var username = new byte[] { };

            try
            {
                username = Base64Url.Decode(opts.Username);
            }
            catch (FormatException)
            {
                username = System.Text.Encoding.UTF8.GetBytes(opts.Username);
            }

            // 1. Get user from DB by username (in our example, auto create missing users)
            var user = DemoStorage.GetOrAddUser(opts.Username, () => new Fido2User
            {
                DisplayName = opts.DisplayName,
                Name = opts.Username,
                Id = username // byte representation of userID is required
            });

            // 2. Get user existing keys by username
            var existingKeys = DemoStorage.GetCredentialsByUser(user).Select(c => c.Descriptor).ToList();

            //var exts = new AuthenticationExtensionsClientInputs() { Extensions = true, UserVerificationIndex = true, Location = true, UserVerificationMethod = true, BiometricAuthenticatorPerformanceBounds = new AuthenticatorBiometricPerfBounds { FAR = float.MaxValue, FRR = float.MaxValue } };
            var exts = new AuthenticationExtensionsClientInputs() { };
            if (null != opts.Extensions
                && null != opts.Extensions.Example)

                exts.Example = opts.Extensions.Example;
            // 3. Create options
            var options = _lib.RequestNewCredential(user, existingKeys, opts.AuthenticatorSelection, opts.Attestation, exts);

            // 4. Temporarily store options, session/in-memory cache/redis/db
            HttpContext.Session.SetString("fido2.attestationOptions", options.ToJson());

            // 5. return options to client
            return Ok(options);
        }

        [HttpPost]
        [Route("/attestation/result")]
        public async Task<IActionResult> MakeCredentialResultTest([FromBody] AuthenticatorAttestationRawResponse attestationResponse)
        {

            // 1. get the options we sent the client
            var jsonOptions = HttpContext.Session.GetString("fido2.attestationOptions");
            var options = CredentialCreateOptions.FromJson(jsonOptions);

            // 2. Create callback so that lib can verify credential id is unique to this user
            IsCredentialIdUniqueToUserAsyncDelegate callback = async (IsCredentialIdUniqueToUserParams args) =>
            {
                var users = await DemoStorage.GetUsersByCredentialIdAsync(args.CredentialId);
                if (users.Count > 0) return false;

                return true;
            };

            // 2. Verify and make the credentials
            var success = await _lib.MakeNewCredentialAsync(attestationResponse, options, callback);

            // 3. Store the credentials in db
            DemoStorage.AddCredentialToUser(options.User, new StoredCredential
            {
                Descriptor = new PublicKeyCredentialDescriptor(success.Result.CredentialId),
                PublicKey = success.Result.PublicKey,
                UserHandle = success.Result.User.Id,
                SignatureCounter = success.Result.Counter
            });

            // 4. return "ok" to the client
            return Ok(success);
        }

        [HttpPost]
        [Route("/assertion/options")]
        public IActionResult AssertionOptionsTest([FromBody] TEST_AssertionClientParams assertionClientParams)
        {
            var username = assertionClientParams.Username;
            // 1. Get user from DB
            var user = DemoStorage.GetUser(username);
            if (user == null) return NotFound("username was not registered");

            // 2. Get registered credentials from database
            var existingCredentials = DemoStorage.GetCredentialsByUser(user).Select(c => c.Descriptor).ToList();

            var uv = assertionClientParams.UserVerification;
            if (null != assertionClientParams.authenticatorSelection) uv = assertionClientParams.authenticatorSelection.UserVerification;

            var exts = new AuthenticationExtensionsClientInputs() { AppID = _origin, SimpleTransactionAuthorization = "FIDO", GenericTransactionAuthorization = new TxAuthGenericArg { ContentType = "text/plain", Content = new byte[] { 0x46, 0x49, 0x44, 0x4F } }, UserVerificationIndex = true, Location = true, UserVerificationMethod = true };
            if (null != assertionClientParams.Extensions
                && null != assertionClientParams.Extensions.Example)

                exts.Example = assertionClientParams.Extensions.Example;

            // 3. Create options
            var options = _lib.GetAssertionOptions(
                existingCredentials,
                uv,
                exts
            );

            // 4. Temporarily store options, session/in-memory cache/redis/db
            HttpContext.Session.SetString("fido2.assertionOptions", options.ToJson());

            // 5. Return options to client
            return Ok(options);
        }

        [HttpPost]
        [Route("/assertion/result")]
        public async Task<IActionResult> MakeAssertionTest([FromBody] AuthenticatorAssertionRawResponse clientResponse)
        {
            // 1. Get the assertion options we sent the client
            var jsonOptions = HttpContext.Session.GetString("fido2.assertionOptions");
            var options = AssertionOptions.FromJson(jsonOptions);

            // 2. Get registered credential from database
            var creds = DemoStorage.GetCredentialById(clientResponse.Id);

            // 3. Get credential counter from database
            var storedCounter = creds.SignatureCounter;

            // 4. Create callback to check if userhandle owns the credentialId
            IsUserHandleOwnerOfCredentialIdAsync callback = async (args) =>
            {
                var storedCreds = await DemoStorage.GetCredentialsByUserHandleAsync(args.UserHandle);
                return storedCreds.Exists(c => c.Descriptor.Id.SequenceEqual(args.CredentialId));
            };

            // 5. Make the assertion
            var res = await _lib.MakeAssertionAsync(clientResponse, options, creds.PublicKey, storedCounter, callback);

            // 6. Store the updated counter
            DemoStorage.UpdateCounter(res.CredentialId, res.Counter);

            var testRes = new
            {
                status = "ok",
                errorMessage = ""
            };

            // 7. return OK to client
            return Ok(testRes);
        }

        private byte[] GetTokenBindingId()
        {
            return Request.HttpContext.Features.Get<ITlsTokenBindingFeature>()?.GetProvidedTokenBindingId();
        }

        /// <summary>
        /// For testing
        /// </summary>
        public class TEST_AssertionClientParams
        {
            public string Username { get; set; }
            public UserVerificationRequirement? UserVerification { get; set; }
            public AuthenticatorSelection authenticatorSelection { get; set; }
            public AuthenticationExtensionsClientOutputs Extensions { get; set; }
        }

        public class TEST_MakeCredentialParams
        {
            public string DisplayName { get; set; }
            public string Username { get; set; }
            public AttestationConveyancePreference Attestation { get; set; }
            public AuthenticatorSelection AuthenticatorSelection { get; set; }
            public AuthenticationExtensionsClientOutputs Extensions { get; set; }
        }
    }

}