﻿using NIST.CVP.ACVTS.Libraries.Crypto.Common.Asymmetric.RSA.Enums;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.Asymmetric.RSA.PrimeGenerators;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.Hash.ShaWrapper;
using NIST.CVP.ACVTS.Libraries.Crypto.RSA.PrimeGenerators;
using NIST.CVP.ACVTS.Libraries.Crypto.SHA.NativeFastSha;
using NIST.CVP.ACVTS.Libraries.Math;
using NIST.CVP.ACVTS.Libraries.Math.Entropy;
using NIST.CVP.ACVTS.Tests.Core.TestCategoryAttributes;
using NUnit.Framework;

namespace NIST.CVP.ACVTS.Libraries.Crypto.RSA.Tests.PrimeGenerators
{
    [TestFixture, LongCryptoTest]
    public class ProvableProbablePrimesWithConditionsGeneratorTests
    {
        [Test]
        [TestCase(0, "010001", "ABCD", new[] { 200, 200, 200, 200 })]
        [TestCase(2048, "03", "ABCD", new[] { 200, 200, 200, 200 })]
        [TestCase(2048, "010001", "ABCD", new[] { 0, 200, 200, 200 })]
        [TestCase(2048, "010001", "ABCD", new[] { 200, 200, 200 })]
        public void ShouldFailWithBadParametersFips186_4(int nlen, string e, string seed, int[] bitlens)
        {
            var param = new PrimeGeneratorParameters
            {
                Modulus = nlen,
                PublicE = new BitString(e).ToPositiveBigInteger(),
                Seed = new BitString(seed),
                BitLens = bitlens
            };

            var sha = new NativeShaFactory().GetShaInstance(new HashFunction(ModeValues.SHA1, DigestSizes.d160));
            var subject = new ProvableProbablePrimesWithConditionsGenerator(sha, new EntropyProvider(new Random800_90()), PrimeTestModes.TwoPow100ErrorBound);

            Assert.Throws<RsaPrimeGenException>(() => subject.GeneratePrimesFips186_4(param));
        }

        [Test]
        [TestCase(2048, ModeValues.SHA1, DigestSizes.d160,
            208, 231, 144, 244, "0100000001", "54c64aa7e8227dfa65668e116df2feb258cbe91ff3deea960288baa3",
            "c6a3628a9d242c96f75faea441c4a7508bace05c1e981242f2b7cda6d2f817fe9fa24cfd29d03a0c876c2ddc39a3b8de8da8477b00e53b6fd6d68f8ea19a177ab5bc51dd3e7d994d2bf49858798d04e37bee50958471dfa850aec16b8b5297f231ddc24695049a747194646cbfd0eddcb0775da8b333a7b22e737e578d7dabcb",
            "cf2e6b79f20e664a7301294417ed4ad6d274afd6289f9e61c9ab38d6e3e45f830c14cf9cbfa9574aa53cc1ac8f52bba40067c2ceaad1acb7a7284f9a2e57cb9548829eb26dbbac5b66c36f331a61e3166ccf375508e82e01b634f803c119db95d0c5ec0669b9eb5ca3d7900cc1c9cbc95132b96035140b7d8753f58362b19a96",
            "c6a3628a9d242c96f75faea441c4a7508bace05c1e981242f2b7cda6d2f817fe9fa24cfd29d03a0c876c2ddc39a3b8de8da8477b00e53b6fd6d68f8ea19a177ab5bc51dd3e7d994ec5b780ffe95fbd972116dbfdeba595f451e8a979feb79ee323592e5e743c02b5e5411694447bb6991c2f51ffe35cc0eb05900bda97aaf9d7",
            "cf2e6b79f20e664a7301294417ed4ad6d274afd6289f9e61c9ab38d6e3e45f830c14cf9cbfa9574aa53cc1ac8f52bba40067c2ceaad1acb7a7284f9a2e57cb9548829eb26dbbac5b66c36f331a61e532cbead1df2db5abfb13cfe42fe29afab14e39266de29756873bbdd96fb8eb289fcbf1e04d9dfab97af54ee869aaf26497"
        )]
        [TestCase(2048, ModeValues.SHA2, DigestSizes.d224,
            248, 192, 192, 232, "0100000001", "0f187bbf5121fe03b2f19d0381b21ad6b448c5a495f5a59eb27969c6",
            "c4bb9e5814323b078e2785e7a3fb1b2791a537725231e563d16f24d82f7ba582968cca78789211d6731826ff8bf8eab2203d2a0c629dc2d9a46906ba626e261ac943317c2fada45eccc3b366740957c7d4fabbf78785e624cd2b381fa7ee9525f0f3633e43e7a072d62c5ef99697e91ea890a3998a8f56b2e01eca5fb42773b7",
            "b9c56d750b2e7dfaf5cbbc1f31e2b1559f77085f4a6bc461ad497c5679653b8fafe95acb5484b750c5a1a27709760ed2976a70ef14835a7e5bc7d2e78dc498999643fadccd74ebebf99cd4ef19657ac7e6b6a1f3f7c69608e2214e35c14912112fac539b37f8c6503dfae2c3077ca524312402f451094a4d76851bea02ae34e4",
            "c4bb9e5814323b078e2785e7a3fb1b2791a537725231e563d16f24d82f7ba582968cca78789211d6731826ff8bf8eab2203d2a0c629dc2d9a46906ba626e261ac943317c2fada46206ea3400db0909287c0fcae955d07a913ed7bcb70507555b966a5f98b73fc476f3026492ed55a5d4b1147c83a12ac4949881389a1c5cb65d",
            "b9c56d750b2e7dfaf5cbbc1f31e2b1559f77085f4a6bc461ad497c5679653b8fafe95acb5484b750c5a1a27709760ed2976a70ef14835a7e5bc7d2e78dc498999643fadccd74ebebf9a0c4fe4c72813699514171702fc0185ac19b8fd5e778c72328a2bf811851e4ed9d83bb58e8d78dbec3bc63c63aadbfbe2b76692a33c0e5"
        )]
        [TestCase(3072, ModeValues.SHA2, DigestSizes.d512,
            440, 190, 560, 189, "0100000001", "cad20639f564334910aed056da3b56f4253c5150632627f8d4f5b8803e941f4d",
            "de530010f038b8dd337a266cd03e08082da825cd60161cfb3d891ec98634257f6e9a3712f48df6cae381f90253d52a828f76c3573e1d596b90316480224768d7519c9f1b9ab59ccd0d2f2e67679cf6c470bd3482483e277a957ec0b49338850efa5d9f4efc90240e89e8a0699f4e2a701daa43d74186ca9b4ff033e759eeeac6ac79e6558e51d33834eeba18ff645701bdba4c6db092ecf6a50aa2d3efc8ea743453b567a7b5dcb5d5c7cc63f12eec87259ad56ef1d055eda084c55cf6c1e0b3",
            "d7faa626e6092e403ade08a16757818a26a9ec76da4b515ee0e6ac6af6676b74fa69c2820bcd03dd0d17057a013bcca553f00ac08c6bda148685ed6ab9905ccf320c9a5965347826b451412797e92351202655c291bc0a213096507339c2f2780a2fb48a7a5fd2e9205cc6629eb81b63a657ee654d9f8087a8cb8eb13d0e57bbcf2c48f0cda1fe07577955a4b57303174efaf7a20b21680156ae799718cde1b754542a2ca2fd3486a27ef05b34206169ec5997bf0e04137572566edcd2e58b26",
            "de530010f038b8dd337a266cd03e08082da825cd60161cfb3d891ec98634257f6e9a3712f48df6cae381f90253d52a828f76c3573e1d596b90316480224768d7519c9f1b9ab59ccd0d2f2e67679cf6c470bd3482483e277a957ec0b49338850efa5d9f4efc90240e89e8a0699f4e2a705bc10fe3e47ccee9dbe14283b71b931f49a562f0cbd6d1fb2ba5f0506ae1a10e6cd5095310dc99c8f7260cad714d301da3b75b36cbf4069198d0cbc79ec0d522d62a02515c1d0b8fe444f889c2be4f23",
            "d7faa626e6092e403ade08a16757818a26a9ec76da4b515ee0e6ac6af6676b74fa69c2820bcd03dd0d17057a013bcca553f00ac08c6bda148685ed6ab9905ccf320c9a5965347826b451412797e92351202655c291bc0a213096507339c2f2780bbef50a0e563e66ea1a1fb994a8a692ac47d77a663085ee82f9ed1e4e66cdf80d71aef30aba7613ea3aea3f2910460bf4174f94ce3ac0d7591a892169858849098c8f1f959344c86e36281129c2e2ef96cd89288c379379bd81fd384fdca6ef"
        )]
        [TestCase(3072, ModeValues.SHA2, DigestSizes.d256,
            392, 357, 344, 273, "0100000001", "00c43e35054d734394b4448eeaeb8724173aa0955357af8545ce24411f62cf7e",
            "fc213d30093bdb30ae037f1ad6ee400001789a883ee431d9cbd5bee2f9c36fd533683342526ffcf03d94552233e816f26d2b8ef96ba1b21ccacc7207ebb0fba031fb3eab934c43984c97681adecaf87848b474ed40e4678f4ffcb97caa1ba4b68537e9e7a048306bb8fc25ce23280ee625146351fe1b0ed78db2522f9334334f570b3a5241988c9cb33a63c8b9026668329c77f7a9d9782ab240b94542e74cb4569a79e8b9bcebc7d10b1c19d288ecab2b3873c25d9743988dc1747d4d6eb3da",
            "cc68a5244b4eb734f1c4d6d3264cdc085d83ad466720a6ac2eddfdab1f8be565b71b17a32b2f9a22ea041605279051c6370ae3996320be4db194b75df97a71d89d82191c26de3ba7e0e8d8796600e77665199d34ff41cc9f460ce56c659a730958bfe3d6e235368a17c974abfeb1ca60132429d8eb479dca96102842c4c3f5f5564ceb1323fe97a515aec8da58fcd7ba058564d7a25ca8197089806fea1d403ebd8cc532c2f9c64b271b879f6366e813bd22ff44c77150911d529f87ab4ee30c",
            "fc213d30093bdb30ae037f1ad6ee400001789a883ee431d9cbd5bee2f9c36fd533683342526ffcf03d94552233e816f26d2b8ef96ba1b21ccacc7207ebb0fba031fb3eab934c43984c97681adecaf87848b474ed40e4678f4ffcb97caa1ba4b685a1a3e45f95b71c440bbfac8df9ece82ee3bbb94660658abae0c77c88c04803650f2380ee17a8ad73a52a2e8d29f61ab9022bf6c63f61d8e49491179c0a6c6d8ef0a3811a1f955408ef854fa908758e853385fbb5e0e64283a2ab98f7250027",
            "cc68a5244b4eb734f1c4d6d3264cdc085d83ad466720a6ac2eddfdab1f8be565b71b17a32b2f9a22ea041605279051c6370ae3996320be4db194b75df97a71d89d82191c26de3ba7e0e8d8796600e77665199d34ff41cc9f460ce56c659a730958bfe3d6e235368a17c974abfeb1ca601357b4e12f49c121d36306c6a23ec95736590d085319ca5ba2802f19469ba26ca171f1eaf00ae8f8b04ac48eb955c91df8d561e8c3652f94e53da89750c3d1517d564aed78b0e4c6d0858920eff875a5"
        )]
        [TestCase(3072, ModeValues.SHA2, DigestSizes.d256,
            480, 189, 200, 190, "0100000001", "e0908d38462ad10362fe250ad20b4dbbeee802a7c596e8221e6b0a169af65daf",
            "d2f505ec914ea43a07f83b215301deccf024983eec2876c67126f8db74b2954eb67901248e8585703f56bd622683e5bd0d8286b450dbdbf3ba0d9dc22fc0dfb6bf4e276d2435c5903d125aaa2d7f6877367d7e4465eb97552f62a1deb9c64979ea2e9e94e4dbc6e41927fffdb19bf322eeaee2b076bcd9bc33d3ec2f1471fa4b180527fb6d551e7ed315160a6fbf3ccdbe1cab07d90acf7956f3acd686d756ffd9e115e55ce2845d8005280cf9b4fb78cd28612c3167076f2fabd9275daff32a",
            "f3fc409be7b7f0af4f6d5909ebfe5fcb5e1ff0ad4dec33b8733ca878cc414ec7f7efe202e998d57a8081f598c2ff5c5c373ad50d86bfb0af3df9f76890e5f929025e12c7f7c57661d97a37c716474419586b4d4361363c2f9f43c8f3f3e6882986f9b12a705cc47f244fc84d6396a3090e3cddd1ae643b67cdd58bc500fd504e8fbc5caf67433a682f04a5b54ec1fde81e0885099b8c3598d20d0dc906b2e24829fef9d2a439a98875bbb0a9cdc223100adece430d291f060edfce41e998247f",
            "d2f505ec914ea43a07f83b215301deccf024983eec2876c67126f8db74b2954eb67901248e8585703f56bd622683e5bd0d8286b450dbdbf3ba0d9dc22fc0dfb6bf4e276d2435c5903d125aaa2d7f6877367d7e4465eb97552f62a1deb9c64979ea2e9e94e4dbc6e4192800488014a6654209284e5182fa425a4b0116b216cd969de266c937046d2029b4be588de8c1e0aad974642bc6ef816475a7be71e9a8b63c87fd802bacabe925ee8a33d987335f4276b44a742128091cac3970250c2e15",
            "f3fc409be7b7f0af4f6d5909ebfe5fcb5e1ff0ad4dec33b8733ca878cc414ec7f7efe202e998d57a8081f598c2ff5c5c373ad50d86bfb0af3df9f76890e5f929025e12c7f7c57661d97a37c716474419586b4d4361363c2f9f43c8f3f3e6882986f9b12a705cc47f244fc84d6396a3090e3cddd1ae643b67cdd58bc500fd504e8fbc5caf67433a682f04a5b54ec3b934baa2116d08db133c8aa1a84fdd37a43f9aeeb4803193396741daca9151488db9bbfa77aa730917490d32193d2ff0127b"
        )]
        public void ShouldCorrectlyGeneratePrimesFips186_4(int nlen, ModeValues mode, DigestSizes dig, int bitlen1, int bitlen2, int bitlen3, int bitlen4, string e, string seed, string xp, string xq, string p, string q)
        {
            var param = new PrimeGeneratorParameters
            {
                Modulus = nlen,
                PublicE = new BitString(e).ToPositiveBigInteger(),
                Seed = new BitString(seed),
                BitLens = new[] { bitlen1, bitlen2, bitlen3, bitlen4 }
            };

            var sha = new NativeShaFactory().GetShaInstance(new HashFunction(mode, dig));
            var entropyProvider = new TestableEntropyProvider();
            entropyProvider.AddEntropy(new BitString(xp).ToPositiveBigInteger());
            entropyProvider.AddEntropy(new BitString(xq).ToPositiveBigInteger());

            var subject = new ProvableProbablePrimesWithConditionsGenerator(sha, entropyProvider, PrimeTestModes.TwoPow100ErrorBound);

            var result = subject.GeneratePrimesFips186_4(param);

            Assert.IsTrue(result.Success, result.ErrorMessage);
            Assert.AreEqual(p.ToUpper(), new BitString(result.Primes.P).ToHex(), "p");
            Assert.AreEqual(q.ToUpper(), new BitString(result.Primes.Q).ToHex(), "q");
        }

        [Test]
        [TestCase(2048, ModeValues.SHA1, DigestSizes.d160,
            208, 231, 144, 244, "0100000001", "54c64aa7e8227dfa65668e116df2feb258cbe91ff3deea960288baa3",
            "c6a3628a9d242c96f75faea441c4a7508bace05c1e981242f2b7cda6d2f817fe9fa24cfd29d03a0c876c2ddc39a3b8de8da8477b00e53b6fd6d68f8ea19a177ab5bc51dd3e7d994d2bf49858798d04e37bee50958471dfa850aec16b8b5297f231ddc24695049a747194646cbfd0eddcb0775da8b333a7b22e737e578d7dabcb",
            "cf2e6b79f20e664a7301294417ed4ad6d274afd6289f9e61c9ab38d6e3e45f830c14cf9cbfa9574aa53cc1ac8f52bba40067c2ceaad1acb7a7284f9a2e57cb9548829eb26dbbac5b66c36f331a61e3166ccf375508e82e01b634f803c119db95d0c5ec0669b9eb5ca3d7900cc1c9cbc95132b96035140b7d8753f58362b19a96",
            "C6A3628A9D242C96F75FAEA441C4A7508BACE05C1E981242F2B7CDA6D2F817FE9FA24CFD29D03A0C876C2DDC39A3B8DE8DA8477B00E53B6FD6D68F8EA19A177AB5BC51DD3E7D994EC5B780FFE95FBD972116DBFDEBA595F451E8A979FEB79EE323592E5E743C02B5E5411694447BB6991C2F51FFE35CC0EB05900BDA97AAF9D7",
            "CF2E6B79F20E664A7301294417ED4AD6D274AFD6289F9E61C9AB38D6E3E45F830C14CF9CBFA9574AA53CC1AC8F52BBA40067C2CEAAD1ACB7A7284F9A2E57CB9548829EB26DBBAC5B66C36F331A61E532CBEAD1DF2DB5ABFB13CFE42FE29AFAB14E39266DE29756873BBDD96FB8EB289FCBF1E04D9DFAB97AF54EE869AAF26497"
        )]
        [TestCase(2048, ModeValues.SHA2, DigestSizes.d224,
            248, 192, 192, 232, "0100000001", "0f187bbf5121fe03b2f19d0381b21ad6b448c5a495f5a59eb27969c6",
            "c4bb9e5814323b078e2785e7a3fb1b2791a537725231e563d16f24d82f7ba582968cca78789211d6731826ff8bf8eab2203d2a0c629dc2d9a46906ba626e261ac943317c2fada45eccc3b366740957c7d4fabbf78785e624cd2b381fa7ee9525f0f3633e43e7a072d62c5ef99697e91ea890a3998a8f56b2e01eca5fb42773b7",
            "b9c56d750b2e7dfaf5cbbc1f31e2b1559f77085f4a6bc461ad497c5679653b8fafe95acb5484b750c5a1a27709760ed2976a70ef14835a7e5bc7d2e78dc498999643fadccd74ebebf99cd4ef19657ac7e6b6a1f3f7c69608e2214e35c14912112fac539b37f8c6503dfae2c3077ca524312402f451094a4d76851bea02ae34e4",
            "C4BB9E5814323B078E2785E7A3FB1B2791A537725231E563D16F24D82F7BA582968CCA78789211D6731826FF8BF8EAB2203D2A0C629DC2D9A46906BA626E261AC943317C2FADA46206EA3400DB0909287C0FCAE955D07A913ED7BCB70507555B966A5F98B73FC476F3026492ED55A5D4B1147C83A12AC4949881389A1C5CB65D",
            "B9C56D750B2E7DFAF5CBBC1F31E2B1559F77085F4A6BC461AD497C5679653B8FAFE95ACB5484B750C5A1A27709760ED2976A70EF14835A7E5BC7D2E78DC498999643FADCCD74EBEBF9A0C4FE4C72813699514171702FC0185AC19B8FD5E778C72328A2BF811851E4ED9D83BB58E8D78DBEC3BC63C63AADBFBE2B76692A33C0E5"
        )]
        [TestCase(3072, ModeValues.SHA2, DigestSizes.d512,
            440, 190, 560, 189, "0100000001", "cad20639f564334910aed056da3b56f4253c5150632627f8d4f5b8803e941f4d",
            "de530010f038b8dd337a266cd03e08082da825cd60161cfb3d891ec98634257f6e9a3712f48df6cae381f90253d52a828f76c3573e1d596b90316480224768d7519c9f1b9ab59ccd0d2f2e67679cf6c470bd3482483e277a957ec0b49338850efa5d9f4efc90240e89e8a0699f4e2a701daa43d74186ca9b4ff033e759eeeac6ac79e6558e51d33834eeba18ff645701bdba4c6db092ecf6a50aa2d3efc8ea743453b567a7b5dcb5d5c7cc63f12eec87259ad56ef1d055eda084c55cf6c1e0b3",
            "d7faa626e6092e403ade08a16757818a26a9ec76da4b515ee0e6ac6af6676b74fa69c2820bcd03dd0d17057a013bcca553f00ac08c6bda148685ed6ab9905ccf320c9a5965347826b451412797e92351202655c291bc0a213096507339c2f2780a2fb48a7a5fd2e9205cc6629eb81b63a657ee654d9f8087a8cb8eb13d0e57bbcf2c48f0cda1fe07577955a4b57303174efaf7a20b21680156ae799718cde1b754542a2ca2fd3486a27ef05b34206169ec5997bf0e04137572566edcd2e58b26",
            "DE530010F038B8DD337A266CD03E08082DA825CD60161CFB3D891EC98634257F6E9A3712F48DF6CAE381F90253D52A828F76C3573E1D596B90316480224768D7519C9F1B9AB59CCD0D2F2E67679CF6C470BD3482483E277A957EC0B49338850EFA5D9F4EFC90240E89E8A0699F4E2A705BC10FE3E47CCEE9DBE14283B71B931F49A562F0CBD6D1FB2BA5F0506AE1A10E6CD5095310DC99C8F7260CAD714D301DA3B75B36CBF4069198D0CBC79EC0D522D62A02515C1D0B8FE444F889C2BE4F23",
            "D7FAA626E6092E403ADE08A16757818A26A9EC76DA4B515EE0E6AC6AF6676B74FA69C2820BCD03DD0D17057A013BCCA553F00AC08C6BDA148685ED6AB9905CCF320C9A5965347826B451412797E92351202655C291BC0A213096507339C2F2780BBEF50A0E563E66EA1A1FB994A8A692AC47D77A663085EE82F9ED1E4E66CDF80D71AEF30ABA7613EA3AEA3F2910460BF4174F94CE3AC0D7591A892169858849098C8F1F959344C86E36281129C2E2EF96CD89288C379379BD81FD384FDCA6EF"
         )]
        [TestCase(3072, ModeValues.SHA2, DigestSizes.d256,
            392, 357, 344, 273, "0100000001", "00c43e35054d734394b4448eeaeb8724173aa0955357af8545ce24411f62cf7e",
            "fc213d30093bdb30ae037f1ad6ee400001789a883ee431d9cbd5bee2f9c36fd533683342526ffcf03d94552233e816f26d2b8ef96ba1b21ccacc7207ebb0fba031fb3eab934c43984c97681adecaf87848b474ed40e4678f4ffcb97caa1ba4b68537e9e7a048306bb8fc25ce23280ee625146351fe1b0ed78db2522f9334334f570b3a5241988c9cb33a63c8b9026668329c77f7a9d9782ab240b94542e74cb4569a79e8b9bcebc7d10b1c19d288ecab2b3873c25d9743988dc1747d4d6eb3da",
            "cc68a5244b4eb734f1c4d6d3264cdc085d83ad466720a6ac2eddfdab1f8be565b71b17a32b2f9a22ea041605279051c6370ae3996320be4db194b75df97a71d89d82191c26de3ba7e0e8d8796600e77665199d34ff41cc9f460ce56c659a730958bfe3d6e235368a17c974abfeb1ca60132429d8eb479dca96102842c4c3f5f5564ceb1323fe97a515aec8da58fcd7ba058564d7a25ca8197089806fea1d403ebd8cc532c2f9c64b271b879f6366e813bd22ff44c77150911d529f87ab4ee30c",
            "FC213D30093BDB30AE037F1AD6EE400001789A883EE431D9CBD5BEE2F9C36FD533683342526FFCF03D94552233E816F26D2B8EF96BA1B21CCACC7207EBB0FBA031FB3EAB934C43984C97681ADECAF87848B474ED40E4678F4FFCB97CAA1BA4B685A1A3E45F95B71C440BBFAC8DF9ECE82EE3BBB94660658ABAE0C77C88C04803650F2380EE17A8AD73A52A2E8D29F61AB9022BF6C63F61D8E49491179C0A6C6D8EF0A3811A1F955408EF854FA908758E853385FBB5E0E64283A2AB98F7250027",
            "CC68A5244B4EB734F1C4D6D3264CDC085D83AD466720A6AC2EDDFDAB1F8BE565B71B17A32B2F9A22EA041605279051C6370AE3996320BE4DB194B75DF97A71D89D82191C26DE3BA7E0E8D8796600E77665199D34FF41CC9F460CE56C659A730958BFE3D6E235368A17C974ABFEB1CA601357B4E12F49C121D36306C6A23EC95736590D085319CA5BA2802F19469BA26CA171F1EAF00AE8F8B04AC48EB955C91DF8D561E8C3652F94E53DA89750C3D1517D564AED78B0E4C6D0858920EFF875A5"
        )]
        [TestCase(3072, ModeValues.SHA2, DigestSizes.d256,
            480, 189, 200, 190, "0100000001", "e0908d38462ad10362fe250ad20b4dbbeee802a7c596e8221e6b0a169af65daf",
            "d2f505ec914ea43a07f83b215301deccf024983eec2876c67126f8db74b2954eb67901248e8585703f56bd622683e5bd0d8286b450dbdbf3ba0d9dc22fc0dfb6bf4e276d2435c5903d125aaa2d7f6877367d7e4465eb97552f62a1deb9c64979ea2e9e94e4dbc6e41927fffdb19bf322eeaee2b076bcd9bc33d3ec2f1471fa4b180527fb6d551e7ed315160a6fbf3ccdbe1cab07d90acf7956f3acd686d756ffd9e115e55ce2845d8005280cf9b4fb78cd28612c3167076f2fabd9275daff32a",
            "f3fc409be7b7f0af4f6d5909ebfe5fcb5e1ff0ad4dec33b8733ca878cc414ec7f7efe202e998d57a8081f598c2ff5c5c373ad50d86bfb0af3df9f76890e5f929025e12c7f7c57661d97a37c716474419586b4d4361363c2f9f43c8f3f3e6882986f9b12a705cc47f244fc84d6396a3090e3cddd1ae643b67cdd58bc500fd504e8fbc5caf67433a682f04a5b54ec1fde81e0885099b8c3598d20d0dc906b2e24829fef9d2a439a98875bbb0a9cdc223100adece430d291f060edfce41e998247f",
            "D2F505EC914EA43A07F83B215301DECCF024983EEC2876C67126F8DB74B2954EB67901248E8585703F56BD622683E5BD0D8286B450DBDBF3BA0D9DC22FC0DFB6BF4E276D2435C5903D125AAA2D7F6877367D7E4465EB97552F62A1DEB9C64979EA2E9E94E4DBC6E4192800488014A6654209284E5182FA425A4B0116B216CD969DE266C937046D2029B4BE588DE8C1E0AAD974642BC6EF816475A7BE71E9A8B63C87FD802BACABE925EE8A33D987335F4276B44A742128091CAC3970250C2E15",
            "F3FC409BE7B7F0AF4F6D5909EBFE5FCB5E1FF0AD4DEC33B8733CA878CC414EC7F7EFE202E998D57A8081F598C2FF5C5C373AD50D86BFB0AF3DF9F76890E5F929025E12C7F7C57661D97A37C716474419586B4D4361363C2F9F43C8F3F3E6882986F9B12A705CC47F244FC84D6396A3090E3CDDD1AE643B67CDD58BC500FD504E8FBC5CAF67433A682F04A5B54EC3B934BAA2116D08DB133C8AA1A84FDD37A43F9AEEB4803193396741DACA9151488DB9BBFA77AA730917490D32193D2FF0127B"
        )]
        [TestCase(4096, ModeValues.SHA2, DigestSizes.d256,
            392, 357, 344, 273, "0100000001", "00c43e35054d734394b4448eeaeb8724173aa0955357af8545ce24411f62cf7e",
            "C0DB6FA164F8D21584C434C062C8D4737B46116D2D0614745AE04AB0E8FE455758C83C609A47D171EF9EA9AB5AC5730B6F5D9EC682CEC40EABE084D107ADA53F53497D0B9423DCA191F5824CA05B36E62E1A9C74B7B6B1E595158D8F12E9EB5F9F10842537B2241B96C1F9EEC08C9752EE0F1A7661C3698C969F201543CAB6201BACCF157DF85A69C390D9DF76E093DBD8E9808B176154AF514EB212D58C31CC4B279EE56697BDCDE275FE1D2E83FAE8E88853EF58580A64358D256E91A63830FCE91A46E23E915E4BB78E6945EB3D0F7D663C93B6E3AA2987FF412D646DCA357EE1413D7F0766E8C5535DDE6E17CDA46A2477E8797B45E935DE522410EC1AFA",
            "D6ACDCCCC3A22935A0C5FAA08E8A87FED9B8F51EB4C5BDFC942C842C7CCF75E958A46F3ACACCB5ADF5BB9805A53BA308D9140825A14B1E58557F3412EC0BE34AAF58D46851431D6A64F7B02EA9AF5DD0BB3774459342533DD00686015FA12A949F07329698703E5E1F06E5AD941FD0F5000C1299F6016A96165639ECA493C3DEDEAF4557743241EF03FCE8BC0D02A22EA143BCE68E628D258AF45C5A18E6E2636DA97FC84C98BC1C39E522E7E3BFAB14E5E48600B8874B091AE9696419E32D9734AC338D304C4158B8551A1FEB09118C1A99C1CA77CA0464502F32C45FD8B617A5AC2AC63853C66C59475698DA1FA27C5D62D85D9995780A80AFE2D2A230095E",
            "C0DB6FA164F8D21584C434C062C8D4737B46116D2D0614745AE04AB0E8FE455758C83C609A47D171EF9EA9AB5AC5730B6F5D9EC682CEC40EABE084D107ADA53F53497D0B9423DCA191F5824CA05B36E62E1A9C74B7B6B1E595158D8F12E9EB5F9F10842537B2241B96C1F9EEC08C9752EE0F1A7661C3698C969F201543CAB6201BACCF157DF85A69C390D9DF76E093DBD8E9808B176154AF514EB212D58C31CC4B94EDDFB8FD4F87994627A72364CC7F6C6DB61DB27A69E72927215F014D5890F417FC2A705AC507971F80EDC4B2FC499E3F4C178EC02D91BF6B6998E491309F33BC87FD728666E83F01B260470446AEE1A96BED5632882867307F4329556E73",
            "D6ACDCCCC3A22935A0C5FAA08E8A87FED9B8F51EB4C5BDFC942C842C7CCF75E958A46F3ACACCB5ADF5BB9805A53BA308D9140825A14B1E58557F3412EC0BE34AAF58D46851431D6A64F7B02EA9AF5DD0BB3774459342533DD00686015FA12A949F07329698703E5E1F06E5AD941FD0F5000C1299F6016A96165639ECA493C3DEDEAF4557743241EF03FCE8BC0D02A22EA143BCE68E628D258AF45C5A18E6E2636DA97FC84C98BC1C39E522E7E3BFAB14E5EB6808A2987F97A7939A89F96675012E93BB5F3A024F4B61CBE0F539875AB4BDE57005B9FC3087F82D308E02BF90E88B2F835C4FD64CBBF9B4C8E2A7FC42B656FB72BCF14D3523151FE0D6E2F1F305"
        )]
        [TestCase(4096, ModeValues.SHA2, DigestSizes.d256,
            480, 189, 200, 190, "0100000001", "e0908d38462ad10362fe250ad20b4dbbeee802a7c596e8221e6b0a169af65daf",
            "C4D2728AC513E73D818071CD4264F881D9D66220B4CF8036B334610BDE7AB774792089EE2329580F6C833B6BE182C4D1D023DBC41C5CA7612E6C4EB7BE3FB2CED20D463EF6A8B23E20DFC3B8BB4A850967E78977C2A591D022C59FED8FFE741DF0BC32FA173FB7696109E66837FC98E8D75715B5B422A3B82E47DF0B1669999B8066AB300130C49FD1D13B24935A26267DCF7DEDA84DCEDC1882BE5C982BC0F7D82F92F1213D39B48C022AE8BB855B8DDB08E95123C98EBDFC23E76FB0B8FA6B08D761C201C55D999480617E8B2B0198B2929A17927DC2FD0C12D4B4453279B020554C8E099B3939CAC45AC1B51F3AFD88C73CB807DDF50073AF6114E7BEC83F",
            "DC0BB605093DF12CFD6670B0151AFFE61FC587603C8B05B5AA596C3E66634655DE9AB592E0D920948A8B3A01A26A6A66C51010EE2A2BDA250C6D79C36D67A9484BCC10B641A702BA3912D0713BAF052B99A0E1F524B03200133B42FBBAE02B49B13B2C3B221506B7CBA21A16371ED269E7AD021D06DA9E2C2E1D2CA46325ACE8627287EAE6C13CBDF17D70810FF92C658CBE492AD5E7057A53608FDF8B9ABB98CDDFDEF58387A990201C2B7310BAAD5E344AF1967FD18BEB0C62ECC31BC5328B3E61F2917022DEB0C8A6A7A75273DDD6E5CF294420ACEE838BCE3697A6D67B8B641C1EC07E204621385AD04A419C2470AA2D3623EE59DB0931B5B5922DAD75E8",
            "C4D2728AC513E73D818071CD4264F881D9D66220B4CF8036B334610BDE7AB774792089EE2329580F6C833B6BE182C4D1D023DBC41C5CA7612E6C4EB7BE3FB2CED20D463EF6A8B23E20DFC3B8BB4A850967E78977C2A591D022C59FED8FFE741DF0BC32FA173FB7696109E66837FC98E8D75715B5B422A3B82E47DF0B1669999B8066AB300130C49FD1D13B24935A26267DCF7DEDA84DCEDC1882BE5C982BC0F7D82F92F1213D39B48C022B21310B6DB20AC1237A55D6C0CF41F05D842E40F42D6D0EAEFBB2CFBF4270DB5F1F4A79BD21F4856B07B893B5DEA6A053F18194003FFE2C9117E7CF75D826E26077CC34B75362518F6A3F0A3B8DF2D73CD823DCB9AB",
            "DC0BB605093DF12CFD6670B0151AFFE61FC587603C8B05B5AA596C3E66634655DE9AB592E0D920948A8B3A01A26A6A66C51010EE2A2BDA250C6D79C36D67A9484BCC10B641A702BA3912D0713BAF052B99A0E1F524B03200133B42FBBAE02B49B13B2C3B221506B7CBA21A16371ED269E7AD021D06DA9E2C2E1D2CA46325ACE8627287EAE6C13CBDF17D70810FF92C658CBE492AD5E7057A53608FDF8B9ABB98CDDFDEF58387A990201C2B7310BAAD5E344AF1967FD18BEB0C62ECC31BC5328B3E61F2917022DEB0C8A6A7A752744725273DCA4252C804F6FB4ACCCF8F18DE2BFCAF16C80EA54E4542FEBDF92A82757193CB7F237E1CAC68628021B05A294541"
        )]
        public void ShouldCorrectlyGeneratePrimesFips186_5(int nlen, ModeValues mode, DigestSizes dig, int bitlen1, int bitlen2, int bitlen3, int bitlen4, string e, string seed, string xp, string xq, string p, string q)
        {
            var param = new PrimeGeneratorParameters
            {
                Modulus = nlen,
                PublicE = new BitString(e).ToPositiveBigInteger(),
                Seed = new BitString(seed),
                BitLens = new[] { bitlen1, bitlen2, bitlen3, bitlen4 }
            };

            var sha = new NativeShaFactory().GetShaInstance(new HashFunction(mode, dig));
            var entropyProvider = new TestableEntropyProvider();
            entropyProvider.AddEntropy(new BitString(xp).ToPositiveBigInteger());
            entropyProvider.AddEntropy(new BitString(xq).ToPositiveBigInteger());

            var subject = new ProvableProbablePrimesWithConditionsGenerator(sha, entropyProvider, PrimeTestModes.TwoPow100ErrorBound);

            var result = subject.GeneratePrimesFips186_5(param);

            Assert.IsTrue(result.Success, result.ErrorMessage);
            Assert.AreEqual(p, new BitString(result.Primes.P).ToHex(), "p");
            Assert.AreEqual(q, new BitString(result.Primes.Q).ToHex(), "q");
        }
    }
}
