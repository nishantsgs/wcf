﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


using Infrastructure.Common;
using System;
using System.ServiceModel;
using System.ServiceModel.Security;
using Xunit;

public partial class Tcp_ClientCredentialTypeTests : ConditionalWcfTest
{
#if FULLXUNIT_NOTSUPPORTED
    [Fact]
#else
    [ConditionalFact(nameof(Root_Certificate_Installed),
                     nameof(Client_Certificate_Installed),
                     nameof(Peer_Certificate_Installed),
                     nameof(SSL_Available))]
#endif
    [OuterLoop]
    public static void NetTcp_SecModeTrans_ClientCredTypeNone_ServerCertValModePeerTrust_EchoString()
    {
#if FULLXUNIT_NOTSUPPORTED
        bool root_Certificate_Installed = Root_Certificate_Installed();
        bool client_Certificate_Installed = Client_Certificate_Installed();
        bool peer_Certificate_Installed = Peer_Certificate_Installed();
        bool ssl_Available = SSL_Available();

        if (!root_Certificate_Installed ||
            !client_Certificate_Installed ||
            !peer_Certificate_Installed ||
            !ssl_Available)
        {
            Console.WriteLine("---- Test SKIPPED --------------");
            Console.WriteLine("Attempting to run the test in ToF, a ConditionalFact evaluated as FALSE.");
            Console.WriteLine("Root_Certificate_Installed evaluated as {0}", root_Certificate_Installed);
            Console.WriteLine("Client_Certificate_Installed evaluated as {0}", client_Certificate_Installed);
            Console.WriteLine("Peer_Certificate_Installed evaluated as {0}", peer_Certificate_Installed);
            Console.WriteLine("SSL_Available evaluated as {0}", ssl_Available);
            return;
        }
#endif
        EndpointAddress endpointAddress = null;
        string testString = "Hello";
        ChannelFactory<IWcfService> factory = null;
        IWcfService serviceProxy = null;

        try
        {
            // *** SETUP *** \\
            NetTcpBinding binding = new NetTcpBinding(SecurityMode.Transport);
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.None;

            endpointAddress = new EndpointAddress(new Uri(Endpoints.NetTcp_SecModeTrans_ClientCredTypeNone_ServerCertValModePeerTrust_Address));

            factory = new ChannelFactory<IWcfService>(binding, endpointAddress);
            factory.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.PeerTrust;

            serviceProxy = factory.CreateChannel();

            // *** EXECUTE *** \\
            string result = serviceProxy.Echo(testString);

            // *** VALIDATE *** \\
            Assert.Equal(testString, result);

            // *** CLEANUP *** \\
            ((ICommunicationObject)serviceProxy).Close();
            factory.Close();
        }
        finally
        {
            // *** ENSURE CLEANUP *** \\
            ScenarioTestHelpers.CloseCommunicationObjects((ICommunicationObject)serviceProxy, factory);
        }
    }

    // This test is expected to validate the service certificate using PeerTrust with X509CertificateValidationMode set to PeerOrChainTrust.
#if FULLXUNIT_NOTSUPPORTED
    [Fact]
#else
    [ConditionalFact(nameof(Root_Certificate_Installed),
                     nameof(Client_Certificate_Installed),
                     nameof(Peer_Certificate_Installed),
                     nameof(SSL_Available))]
#endif
    [OuterLoop]
    public static void NetTcp_SecModeTrans_ClientCredTypeNone_ServerCertValModePeerOrChainTrust_EchoString()
    {
#if FULLXUNIT_NOTSUPPORTED
        bool root_Certificate_Installed = Root_Certificate_Installed();
        bool client_Certificate_Installed = Client_Certificate_Installed();
        bool peer_Certificate_Installed = Peer_Certificate_Installed();
        bool ssl_Available = SSL_Available();

        if (!root_Certificate_Installed || 
            !client_Certificate_Installed ||
            !peer_Certificate_Installed ||
            !ssl_Available)
        {
            Console.WriteLine("---- Test SKIPPED --------------");
            Console.WriteLine("Attempting to run the test in ToF, a ConditionalFact evaluated as FALSE.");
            Console.WriteLine("Root_Certificate_Installed evaluated as {0}", root_Certificate_Installed);
            Console.WriteLine("Client_Certificate_Installed evaluated as {0}", client_Certificate_Installed);
            Console.WriteLine("Peer_Certificate_Installed evaluated as {0}", peer_Certificate_Installed);
            Console.WriteLine("SSL_Available evaluated as {0}", ssl_Available);
            return;
        }
#endif
        EndpointAddress endpointAddress = null;
        string testString = "Hello";
        ChannelFactory<IWcfService> factory = null;
        IWcfService serviceProxy = null;

        try
        {
            // *** SETUP *** \\
            NetTcpBinding binding = new NetTcpBinding(SecurityMode.Transport);
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.None;

            endpointAddress = new EndpointAddress(new Uri(Endpoints.NetTcp_SecModeTrans_ClientCredTypeNone_ServerCertValModePeerTrust_Address));

            factory = new ChannelFactory<IWcfService>(binding, endpointAddress);
            factory.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.PeerOrChainTrust;

            serviceProxy = factory.CreateChannel();

            // *** EXECUTE *** \\
            string result = serviceProxy.Echo(testString);

            // *** VALIDATE *** \\
            Assert.Equal(testString, result);

            // *** CLEANUP *** \\
            ((ICommunicationObject)serviceProxy).Close();
            factory.Close();
        }
        finally
        {
            // *** ENSURE CLEANUP *** \\
            ScenarioTestHelpers.CloseCommunicationObjects((ICommunicationObject)serviceProxy, factory);
        }
    }
}
