﻿using NIST.CVP.ACVTS.Libraries.Crypto.Common.Asymmetric.RSA.Enums;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.Asymmetric.RSA.PrimeGenerators;
using NIST.CVP.ACVTS.Libraries.Crypto.RSA.PrimeGenerators;
using NIST.CVP.ACVTS.Libraries.Math;
using NIST.CVP.ACVTS.Libraries.Math.Entropy;
using NIST.CVP.ACVTS.Tests.Core.TestCategoryAttributes;
using NUnit.Framework;

namespace NIST.CVP.ACVTS.Libraries.Crypto.RSA.Tests.PrimeGenerators
{
    [TestFixture, LongCryptoTest]
    public class AllProbablePrimesWithConditionsGeneratorTests
    {
        [Test]
        [TestCase(0, "010001", "ABCD", new[] { 200, 200, 200, 200 })]
        // 1024 is allowed for 186_4 for sigVer only. The keys still need to be generated under these parameters
        //[TestCase(1024, "0100000001", "5c029cd058da46698662234f46ca7fc9eabe138c", new[] {208, 231, 144, 244})]
        //[TestCase(1024, "d70ff9", "5c029cd058da46698662234f46ca7fc9eabe138c", new[] {208, 231, 144, 244})]
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

            var entropyProvider = new EntropyProvider(new Random800_90());
            var subject = new AllProbablePrimesWithConditionsGenerator(entropyProvider, PrimeTestModes.TwoPow100ErrorBound);

            Assert.Throws<RsaPrimeGenException>(() => subject.GeneratePrimesFips186_4(param));
        }

        [Test]
        [TestCase(2048, 200, 176, 352, 144, "0100000001",       // Line 31391
            "3bca340579017b28e5e162f455e4d61c72e7701915214f5051",
            "16a0e3ad12cad2f7789dde4b1612f0e366cf943244f7",
            "cfd9fa055727e42f85b1799036a4ac82fd65075bb333266f28d68a2bed7a09e7bf0cdd82ce8a4a86fd373db1aad256155c15a1c9fba5413c1e7ead6a0e71f8898dbb5f1d2131e3433de7fb658fd46cf4c1e914dd423bbfe2b6e00507b088954d29b4ca0ed0c5d78f19e91dc0420a448785f28327e6618182db7963e3f5c75ba8",
            "e30b4afef880afdff67cf23693919d70a10f8f816120b7d66a6aa119a9a9e5283bb3ac080624892d595ecc6f",
            "161f14d96abeeb7ba4c76c0ceb6c0aa457b7",
            "f8a240d2a81f380bcdeae5ce6a375e92b6388cb43661225eeec7ae2a016fd740f3e762189060797e07f4bbcb426e9ea4020cdd04cd007191b5c9730f8649133238daebc7964fdd5b8a967a791a89810b515073972cfa7745374ca75cdf3540a1be60751cc498b8e2098d59a2ffe256d38241255a6c8d312c07ed5f12f3530440",
            "cfd9fa055727e42f85b1799036a4ac82fd65075bb333266f28d68a2bed7a09e7bf0cdd82ce8a4a86fd373db1aad256155c15a1c9fba5413c1e7ead6a0e71f8898dbb5f1d2131e3433de7fb658fd46cf4cdd2fecf1feea0a42e40638497065c6c415160109adfbfd93df218d5ad96bbd2d313878e74541403eef05a5f276791f7",
            "f8a240d2a81f380bcdeae5ce6a375e92b6388cb43661225eeec7ae2a016fd740f3e762189060797e07f4bbcb426e9ea4020cdd04cd007191b5c9730f86491332390802159c1f607b33e3e9cac50252fed3df71c68862a5fe789c7122a7a6e9a058d41b1a5e275aaeb2d9748a0ecd65c7f13fa42dd882bc7dece3bd2153e1949b"
        )]
        [TestCase(2048, 216, 144, 280, 176, "0100000001",
            "0831ae7b2c7d8bee85ec6247bd044249d8728ad230a519fde6ace5",
            "3de67f42d8d8b93ad04c24f6bc27d2b52319",
            "ef207ff9176b61275de0dc6d8741e3a89d06fc09b24650d57b625be2a72cc86925751de2216b3812337818a3a09f33d17aa39035665c43e327fea74c87c013cdbad72115ac0340ce00c4c81a8dcea81f9ac158db347c8ad4856ea009b1800a70c78fd87107415c1aae8adce4cc7abe75872ecb682e7594eef39ed4d24451cf13",
            "e35bec9b3a8f41e380f638694aaf7b481c79528384fb60cbb514fde7fb885602366f59",
            "02e51d45906cb388c963212e2211500a42f799a2ea89",
            "fd39115c29fc5a755af9e1832574ee91f84517e97de018cd72c0aeb17600f9b0fcb3dbf17ac824a0c2d3fa58c4280c2eacae3a5ea28270d45bac53157826a8f116c01361579d33276120f1e0f4c5b666ec8993a608a7b917f7aee392fbdd5f1fb5adb817fa9e004137eb4e2e78428ce8830ed1e7cac2eda2dc6ef00dee377d63",
            "ef207ff9176b61275de0dc6d8741e3a89d06fc09b24650d57b625be2a72cc86925751de2216b3812337818a3a09f33d17aa39035665c43e327fea74c87c013cdbad72115ac0340ce00c4c81a8dcea81f9ac15ce3ed453a5b3df87361f73337f8630c8b0cc25c2c3dfb33ffde7669e3352f71be2ebc878a8f93ce88cb5e77fb0b",
            "fd39115c29fc5a755af9e1832574ee91f84517e97de018cd72c0aeb17600f9b0fcb3dbf17ac824a0c2d3fa58c4280c2eacae3a5ea28270d45bac53157826a8f116c01361579d4a25764c9f51462b8f6bc780e21e2c530110f4f86194aeaf367e698c1c81107832a9b620549074a64f03d8b649ea6339d0e4988753ee4914f147"
        )]
        [TestCase(3072, 528, 208, 544, 192, "0100000001",
            "8f30b941465f2ae92200867ba5b90e6aa3c704a79d657e4ba7a9701c4c59c7cf952965d693b7e506d4b0856f8ad75c9d38db0163c27753905566174d7df30f111677",
            "7ee5a347a6c0b9a733cee629d5ee3f265f925150ccf3b8d9ccb3",
            "f57f3544326ab0cb2da6a8bf8a28b0830ba70a50fafb53514bc443efcb3f28222cac63b5536cf7e2cb2a2427ced3251193b5092de923bfe26254eb798c296d0feb2ba7e8c09d282047f8cb15353f6ad4268b46570fca1338f5b5bab9aeafb905623f758824a4f1c3e43a86c62acc4fa9e4b6f91dba59d41b34b8549477f17cabc143d1a58e80bd432f6d69b59005b86476917c2d6946ada1f084a2f906ad840814be5004306a4a87b36d8df632dd43da45899b6a041f0554c3331d677224ee95",
            "b77afdae8d1a401375d2d940c3625eab39b761c508067e4335bfeb47f4027f301e70e7d3edfd4e9e6e353f61699045ab07241976e64631d47285db16167befc6380d40f3",
            "1c5dc9f839851150af8910c83320ba14f76edeeee7eb4abf",
            "b6bbf9ef00fbd60aee10b2bb4c57e19cdf9e05c4b4489ab0592cf6c1fbbe5b7f02720e01c6943c661f7f3607570368a9bdf51dbe027d3f00805a6058079d2b55dbe1649c3154727d80832e0f43a50d2dd104ff080d9c732b82e77a739f1f07189b54ba66948bff8a5adf93a3121bebe39275b7e0886aa30c7974c7c69fb7e7ef4f5d91ee572aa3e2efc262246772afade2f1501b50627c56447c10887e289fccb5d089409bdc45f372958d5b78e82a9b41ee6b111ff6988608dceca0a0e74646",
            "f57f3544326ab0cb2da6a8bf8a28b0830ba70a50fafb53514bc443efcb3f28222cac63b5536cf7e2cb2a2427ced3251193b5092de923bfe26254eb798c296d0feb2ba7e8c09d282047f8cb15353f6ad4268b46570fca1338f5b5bab9aeafb905623f761665e71b5a8eb64a5d037fa6eb1b1143d9c9dad0fa70eba170a2dc94d679ce57d911c8e69030672a86f1a1e0619177b6c6bbb55b3e2b74a8b97ce1798a4060fc6aac8d8b7991e7b2baefce026f59aa19db386e16f3c5d6ec3111d1c107",
            "b6bbf9ef00fbd60aee10b2bb4c57e19cdf9e05c4b4489ab0592cf6c1fbbe5b7f02720e01c6943c661f7f3607570368a9bdf51dbe027d3f00805a6058079d2b55dbe1649c3154727d80832e0f43a50d2dd104ff080d9c732b82e77a739f1f07189b54ba8026ec218290b256c272a8060e38bb5a7c4aab6534c5f907cd5d518a2a72ba240ca460d03b6d8ba044bbd4015e6d55f9902014c4a5571cfbaeea018c3d58556d7a65d0877bf298383fda800f824e314634cbad7ff4ad934e21b3528ca5"
        )]
        [TestCase(3072, 456, 176, 384, 360, "957a31",
            "0db49da0bbfbc493e76e0de28dbeb77c9e0cba0637964c2d3a0496e7bd61893cac9db8835b89c2d9a20a2fada2badf1b04ec794a08a65db431",
            "7b17c1d883d74bbf56ef7f5f7edff92b166984678c5f",
            "fac2da5142d21b94d6c8940bd88c5f2c7a2cb8f01aa1f3505d218e9f08c4f241754fafa226858257f8e13fd24b549d65f638a3b93aecd0bb9c0e9fa4e46a74a208403a836d1885657ca9abf0fd61d56e6031c7317752f6f0eea6163f99d538ab4bb13a97e66e2fa10218af191ac15d18b973b33ed6147f5b72dc66bf965b2ef94c5db61802bc2ac33fc1d61ff89e17e9a3114819b101611dd975a13a1100b363a61918f315a5f2871549866b86f5e1682ad4dbbf451afa398dbb3051b1c61555",
            "ed02a9f6cad62a2eb74ee2fa9198935eb1f6220fb67f94d14495abbcc741d9709c2dcd2a2b9876b48fc591a9d9752f67",
            "35f8b4af8d2422143091d5240cc844bb561ae2ea44220672fdc175deb4b5b58fab73f814fa53dc14a42c015831",
            "ce800174dd58fa6b980e59b03033feafcdb9920fce453a0134bdfe137852c944b4e588bb442d0104d6634f95e590a2b16b5dab73ae4d27495b01c45e837c501e34210ec6dc2ac2e73918ae114dfc01664fea913749ef7960947072d646b03da3ac8a0b695b772af9d50556d62bd5aefa1cddf4bdf3ee82d7f0318b1921f94b0f0e60fca8e0dd4358f27b71e7d5d5b0a87fc8353dc032f3ff5c1f469ee32475cd0e8b008195c0b6f4b063e76a505f65c3aa1f49d448fff5c22fd44b2979bea250",
            "fac2da5142d21b94d6c8940bd88c5f2c7a2cb8f01aa1f3505d218e9f08c4f241754fafa226858257f8e13fd24b549d65f638a3b93aecd0bb9c0e9fa4e46a74a208403a836d1885657ca9abf0fd61d56e6031c7317752f6f0eea6163f99d538ab4bb13a97e66e2fa10218af191ac15d18e4904a1af3e62e3654628c7b2ae29712945f99e8a6b0272805dbaadd83990d7186f5be2711a286023365e6fd72920b2de5d29748095c49ec631d5d5eba61925ebafbe73eba61186936af638c1e9ddd1d",
            "ce800174dd58fa6b980e59b03033feafcdb9920fce453a0134bdfe137852c944b4e588bb442d0104d6634f95e590a2b16b5dab73ae4d27495b01c45e837c501e34210ec6dc2ac2e73918ae114dfc01664fea913749ef7960947072d646b03da3ac8d3e42c179be675f81f8533ac22d24752f8dd48064bceaa8b6e1945dd0de52912bb3f544b58825c372d59e3f5bb6f0b6d7470fd4c5efee507f38a40d55e74791677e80271d89ef09d1f2958eb249d788038f5c16b1f66b1febd6ee7d1fbc9d"
        )]
        public void ShouldCorrectlyGeneratePrimesFips186_4(int nlen, int bitlen1, int bitlen2, int bitlen3, int bitlen4, string e, string xp1, string xp2, string xp, string xq1, string xq2, string xq, string p, string q)
        {
            var param = new PrimeGeneratorParameters
            {
                Modulus = nlen,
                PublicE = new BitString(e).ToPositiveBigInteger(),
                BitLens = new[] { bitlen1, bitlen2, bitlen3, bitlen4 }
            };

            var entropyProvider = new TestableEntropyProvider();
            entropyProvider.AddEntropy(new BitString(xp1, bitlen1));
            entropyProvider.AddEntropy(new BitString(xp2, bitlen2));
            entropyProvider.AddEntropy(new BitString(xp).ToPositiveBigInteger());
            entropyProvider.AddEntropy(new BitString(xq1, bitlen3));
            entropyProvider.AddEntropy(new BitString(xq2, bitlen4));
            entropyProvider.AddEntropy(new BitString(xq).ToPositiveBigInteger());

            var subject = new AllProbablePrimesWithConditionsGenerator(entropyProvider, PrimeTestModes.TwoPow100ErrorBound);

            var result = subject.GeneratePrimesFips186_4(param);

            Assert.IsTrue(result.Success, result.ErrorMessage);
            Assert.AreEqual(p.ToUpper(), new BitString(result.Primes.P).ToHex(), "p");
            Assert.AreEqual(q.ToUpper(), new BitString(result.Primes.Q).ToHex(), "q");
        }

        [Test]
        [TestCase(2048, 200, 176, 352, 144, "0100000001",
            "3bca340579017b28e5e162f455e4d61c72e7701915214f5051",
            "16a0e3ad12cad2f7789dde4b1612f0e366cf943244f7",
            "cfd9fa055727e42f85b1799036a4ac82fd65075bb333266f28d68a2bed7a09e7bf0cdd82ce8a4a86fd373db1aad256155c15a1c9fba5413c1e7ead6a0e71f8898dbb5f1d2131e3433de7fb658fd46cf4c1e914dd423bbfe2b6e00507b088954d29b4ca0ed0c5d78f19e91dc0420a448785f28327e6618182db7963e3f5c75ba8",
            "e30b4afef880afdff67cf23693919d70a10f8f816120b7d66a6aa119a9a9e5283bb3ac080624892d595ecc6f",
            "161f14d96abeeb7ba4c76c0ceb6c0aa457b7",
            "f8a240d2a81f380bcdeae5ce6a375e92b6388cb43661225eeec7ae2a016fd740f3e762189060797e07f4bbcb426e9ea4020cdd04cd007191b5c9730f8649133238daebc7964fdd5b8a967a791a89810b515073972cfa7745374ca75cdf3540a1be60751cc498b8e2098d59a2ffe256d38241255a6c8d312c07ed5f12f3530440",
            "CFD9FA055727E42F85B1799036A4AC82FD65075BB333266F28D68A2BED7A09E7BF0CDD82CE8A4A86FD373DB1AAD256155C15A1C9FBA5413C1E7EAD6A0E71F8898DBB5F1D2131E3433DE7FB658FD46CF4CDD2FECF1FEEA0A42E40638497065C6C415160109ADFBFD93DF218D5AD96BBD2D313878E74541403EEF05A5F276791F7",
            "F8A240D2A81F380BCDEAE5CE6A375E92B6388CB43661225EEEC7AE2A016FD740F3E762189060797E07F4BBCB426E9EA4020CDD04CD007191B5C9730F86491332390802159C1F607B33E3E9CAC50252FED3DF71C68862A5FE789C7122A7A6E9A058D41B1A5E275AAEB2D9748A0ECD65C7F13FA42DD882BC7DECE3BD2153E1949B"
        )]
        [TestCase(2048, 216, 144, 280, 176, "0100000001",
            "0831ae7b2c7d8bee85ec6247bd044249d8728ad230a519fde6ace5",
            "3de67f42d8d8b93ad04c24f6bc27d2b52319",
            "ef207ff9176b61275de0dc6d8741e3a89d06fc09b24650d57b625be2a72cc86925751de2216b3812337818a3a09f33d17aa39035665c43e327fea74c87c013cdbad72115ac0340ce00c4c81a8dcea81f9ac158db347c8ad4856ea009b1800a70c78fd87107415c1aae8adce4cc7abe75872ecb682e7594eef39ed4d24451cf13",
            "e35bec9b3a8f41e380f638694aaf7b481c79528384fb60cbb514fde7fb885602366f59",
            "02e51d45906cb388c963212e2211500a42f799a2ea89",
            "fd39115c29fc5a755af9e1832574ee91f84517e97de018cd72c0aeb17600f9b0fcb3dbf17ac824a0c2d3fa58c4280c2eacae3a5ea28270d45bac53157826a8f116c01361579d33276120f1e0f4c5b666ec8993a608a7b917f7aee392fbdd5f1fb5adb817fa9e004137eb4e2e78428ce8830ed1e7cac2eda2dc6ef00dee377d63",
            "EF207FF9176B61275DE0DC6D8741E3A89D06FC09B24650D57B625BE2A72CC86925751DE2216B3812337818A3A09F33D17AA39035665C43E327FEA74C87C013CDBAD72115AC0340CE00C4C81A8DCEA81F9AC15CE3ED453A5B3DF87361F73337F8630C8B0CC25C2C3DFB33FFDE7669E3352F71BE2EBC878A8F93CE88CB5E77FB0B",
            "FD39115C29FC5A755AF9E1832574EE91F84517E97DE018CD72C0AEB17600F9B0FCB3DBF17AC824A0C2D3FA58C4280C2EACAE3A5EA28270D45BAC53157826A8F116C01361579D4A25764C9F51462B8F6BC780E21E2C530110F4F86194AEAF367E698C1C81107832A9B620549074A64F03D8B649EA6339D0E4988753EE4914F147"
        )]
        [TestCase(3072, 528, 208, 544, 192, "0100000001",
            "8f30b941465f2ae92200867ba5b90e6aa3c704a79d657e4ba7a9701c4c59c7cf952965d693b7e506d4b0856f8ad75c9d38db0163c27753905566174d7df30f111677",
            "7ee5a347a6c0b9a733cee629d5ee3f265f925150ccf3b8d9ccb3",
            "f57f3544326ab0cb2da6a8bf8a28b0830ba70a50fafb53514bc443efcb3f28222cac63b5536cf7e2cb2a2427ced3251193b5092de923bfe26254eb798c296d0feb2ba7e8c09d282047f8cb15353f6ad4268b46570fca1338f5b5bab9aeafb905623f758824a4f1c3e43a86c62acc4fa9e4b6f91dba59d41b34b8549477f17cabc143d1a58e80bd432f6d69b59005b86476917c2d6946ada1f084a2f906ad840814be5004306a4a87b36d8df632dd43da45899b6a041f0554c3331d677224ee95",
            "b77afdae8d1a401375d2d940c3625eab39b761c508067e4335bfeb47f4027f301e70e7d3edfd4e9e6e353f61699045ab07241976e64631d47285db16167befc6380d40f3",
            "1c5dc9f839851150af8910c83320ba14f76edeeee7eb4abf",
            "b6bbf9ef00fbd60aee10b2bb4c57e19cdf9e05c4b4489ab0592cf6c1fbbe5b7f02720e01c6943c661f7f3607570368a9bdf51dbe027d3f00805a6058079d2b55dbe1649c3154727d80832e0f43a50d2dd104ff080d9c732b82e77a739f1f07189b54ba66948bff8a5adf93a3121bebe39275b7e0886aa30c7974c7c69fb7e7ef4f5d91ee572aa3e2efc262246772afade2f1501b50627c56447c10887e289fccb5d089409bdc45f372958d5b78e82a9b41ee6b111ff6988608dceca0a0e74646",
            "F57F3544326AB0CB2DA6A8BF8A28B0830BA70A50FAFB53514BC443EFCB3F28222CAC63B5536CF7E2CB2A2427CED3251193B5092DE923BFE26254EB798C296D0FEB2BA7E8C09D282047F8CB15353F6AD4268B46570FCA1338F5B5BAB9AEAFB905623F761665E71B5A8EB64A5D037FA6EB1B1143D9C9DAD0FA70EBA170A2DC94D679CE57D911C8E69030672A86F1A1E0619177B6C6BBB55B3E2B74A8B97CE1798A4060FC6AAC8D8B7991E7B2BAEFCE026F59AA19DB386E16F3C5D6EC3111D1C107",
            "B6BBF9EF00FBD60AEE10B2BB4C57E19CDF9E05C4B4489AB0592CF6C1FBBE5B7F02720E01C6943C661F7F3607570368A9BDF51DBE027D3F00805A6058079D2B55DBE1649C3154727D80832E0F43A50D2DD104FF080D9C732B82E77A739F1F07189B54BA8026EC218290B256C272A8060E38BB5A7C4AAB6534C5F907CD5D518A2A72BA240CA460D03B6D8BA044BBD4015E6D55F9902014C4A5571CFBAEEA018C3D58556D7A65D0877BF298383FDA800F824E314634CBAD7FF4AD934E21B3528CA5"
        )]
        [TestCase(3072, 456, 176, 384, 360, "957a31",
            "0db49da0bbfbc493e76e0de28dbeb77c9e0cba0637964c2d3a0496e7bd61893cac9db8835b89c2d9a20a2fada2badf1b04ec794a08a65db431",
            "7b17c1d883d74bbf56ef7f5f7edff92b166984678c5f",
            "fac2da5142d21b94d6c8940bd88c5f2c7a2cb8f01aa1f3505d218e9f08c4f241754fafa226858257f8e13fd24b549d65f638a3b93aecd0bb9c0e9fa4e46a74a208403a836d1885657ca9abf0fd61d56e6031c7317752f6f0eea6163f99d538ab4bb13a97e66e2fa10218af191ac15d18b973b33ed6147f5b72dc66bf965b2ef94c5db61802bc2ac33fc1d61ff89e17e9a3114819b101611dd975a13a1100b363a61918f315a5f2871549866b86f5e1682ad4dbbf451afa398dbb3051b1c61555",
            "ed02a9f6cad62a2eb74ee2fa9198935eb1f6220fb67f94d14495abbcc741d9709c2dcd2a2b9876b48fc591a9d9752f67",
            "35f8b4af8d2422143091d5240cc844bb561ae2ea44220672fdc175deb4b5b58fab73f814fa53dc14a42c015831",
            "ce800174dd58fa6b980e59b03033feafcdb9920fce453a0134bdfe137852c944b4e588bb442d0104d6634f95e590a2b16b5dab73ae4d27495b01c45e837c501e34210ec6dc2ac2e73918ae114dfc01664fea913749ef7960947072d646b03da3ac8a0b695b772af9d50556d62bd5aefa1cddf4bdf3ee82d7f0318b1921f94b0f0e60fca8e0dd4358f27b71e7d5d5b0a87fc8353dc032f3ff5c1f469ee32475cd0e8b008195c0b6f4b063e76a505f65c3aa1f49d448fff5c22fd44b2979bea250",
            "FAC2DA5142D21B94D6C8940BD88C5F2C7A2CB8F01AA1F3505D218E9F08C4F241754FAFA226858257F8E13FD24B549D65F638A3B93AECD0BB9C0E9FA4E46A74A208403A836D1885657CA9ABF0FD61D56E6031C7317752F6F0EEA6163F99D538AB4BB13A97E66E2FA10218AF191AC15D18E4904A1AF3E62E3654628C7B2AE29712945F99E8A6B0272805DBAADD83990D7186F5BE2711A286023365E6FD72920B2DE5D29748095C49EC631D5D5EBA61925EBAFBE73EBA61186936AF638C1E9DDD1D",
            "CE800174DD58FA6B980E59B03033FEAFCDB9920FCE453A0134BDFE137852C944B4E588BB442D0104D6634F95E590A2B16B5DAB73AE4D27495B01C45E837C501E34210EC6DC2AC2E73918AE114DFC01664FEA913749EF7960947072D646B03DA3AC8D3E42C179BE675F81F8533AC22D24752F8DD48064BCEAA8B6E1945DD0DE52912BB3F544B58825C372D59E3F5BB6F0B6D7470FD4C5EFEE507F38A40D55E74791677E80271D89EF09D1F2958EB249D788038F5C16B1F66B1FEBD6EE7D1FBC9D"
        )]
        [TestCase(4096, 528, 208, 544, 192, "0100000001",
            "80BF5FD73A43E8187E090247544492870D77DF8C6FC80C6AA3A518391B0656B06B9C5E674DF98A53A8AEF651A3D5DD7D06828239752AEADF27ACD1C49790C6745BA5",
            "884DAE24481AD5C6EC12731B3F2E4539323864CC677BE2C5BE4F",
            "E45C13AF99900530E26BB3647E4B3DD6B83C9712B54349CEBD9033E9E40C6C8D1CC82AC1950D7806EA1A1422B470712D89503C2B011B0FA08D8056AB32DC3D077221CD2738A7317CEDF973217073301CB82E03068CFCBDFAB1A772DFB1EC844BF14A1178183A5B9D1B34A47A20F3BC19DD81F73B7EAD6A38C0CEC1F048A40325AAFF0C095AE301D09C1655B66918D89845D3AB782089943E7FB9EE99C49B38CC95C3618B8B296FE8405BEA78A7B41251A0193EFB32EFACB62CEEABFD97A984F5DDDE214F833ADFB741BD1313BE222DC378A9C8CF8CCB9F7A2C64E4A04F16EA161046D3A86319132D931451AE575D853F3F94AF0D5E59715C94C96B72520834E4",
            "B6C7F6C7439651C66B13F88C070819FDC196A4B7707ADDD295ED936ABCDED4F0AB6358496054D9D6C3B44FE54D20C1A63E23B9080DE93B0E10561C1C6D157ABBF945ACC9",
            "F81F5C4F30639242320C43D5087A8AD413D466DB331C3ED9",
            "CA3FA20883FA0174FE408C91CD14AAF4D361F0744D4B8BF9A21DF68747C87F9E5FD19BF58B5AAD1E93637B1EBE3A04DD27DD03FB74BA82F8A503FDCAB381C8894504C308AFB3DD5FE3C7742BCFCBDEA8667144F7CBE1B3339C91B280E41669F6431067316E7D4D7E993C59E1FA51FC700359419FCC8CB35F5C1BE75B35CCEDD1E1D875A90AB766C845A6243E75A85FA99A007693F7A418F7E6BE7B55FF36AA1A23855AEBFABDF3D8357B6B4B41623F79A8784359A9671AE667C6E1315B039E20404757883BCA510FCDFDA7590AE1C15FB1F9DE21519F563F2814D2803C251BA0751365D3AD7F8C4B44DC83DB93C86567F6B5F20342D667512A4B0BA1FF2A56A1",
            "E45C13AF99900530E26BB3647E4B3DD6B83C9712B54349CEBD9033E9E40C6C8D1CC82AC1950D7806EA1A1422B470712D89503C2B011B0FA08D8056AB32DC3D077221CD2738A7317CEDF973217073301CB82E03068CFCBDFAB1A772DFB1EC844BF14A1178183A5B9D1B34A47A20F3BC19DD81F73B7EAD6A38C0CEC1F048A40325AAFF0C095AE301D09C1655B66918D89845D3AB782089943E7FB9EE99C49B38CC95C3627D7546C58DAA4EE03E20214F60B3751BC5CB25A16743E8FC5902D56769C210B370CA5E84C8F89B7B7FCC53098CF6444DA915B4DF1987222B2837B3A215547E60B0106C61E5923AAAA42A99BCD6FC2E2955D9B5DBEED6AC976D3774DCCF",
            "CA3FA20883FA0174FE408C91CD14AAF4D361F0744D4B8BF9A21DF68747C87F9E5FD19BF58B5AAD1E93637B1EBE3A04DD27DD03FB74BA82F8A503FDCAB381C8894504C308AFB3DD5FE3C7742BCFCBDEA8667144F7CBE1B3339C91B280E41669F6431067316E7D4D7E993C59E1FA51FC700359419FCC8CB35F5C1BE75B35CCEDD1E1D875A90AB766C845A6243E75A85FA99A007693F7A418F7E6BE7B55FF36AA1A238567D6E094496C9AC7684A1E83AB2ED2BABD2B25FB3F05579DD3E5F7086EE9EFE84623BA5E23E860D91F2093645B8B6ACC4BCAA684810436EF6FE09D388C0DC8FF313B01980B63BD663FA004598D9070E395D87DA0A6AD9367353D8E71C9B1"
        )]
        [TestCase(4096, 456, 176, 384, 360, "957a31",
            "BB0BF173DD435945E993F24DC27D004AC22986C34126365898A778DAB7D01FBBFFD9E245C96856B9208632CE40B3B54C4243B8D4105A219E51",
            "1664447EA52D325D440C98E81A0ED4688CE7B4555B49",
            "B69D3985CD89B8C48099B365ABD2906494392331BFBC8CAB687ECC3BBB98F29FA6D9204BB02E68C0785C437A7C5E6C5B56E0C12FBA36176A57C14ECEE61E12D5F8E9BB4FEBC750DCED3EAE32B1804EBECA7996B4E9089A71EA521D1933DB851444BB775EA849D3FE5C15B50DB2352E8E58FB09EE86D6AB246F4CFA1DCA03443585CA5EBCDE7B1DF59BB12C5261A2CF0B1DFF443A1F6428E3A4A26D45F81434E411CB80133D750FFC7C6C79157618E87ECA83E378A9ABC4B081F3AB214401BC841484211D3B5BBC34EACC922B930BD8E785D5CD3891FE5CF6033A8B2F175F0DD239AB5921ECFB6D5B66BE02E2904D543AD35C1BDE73B86318B5531C839FB95D3D",
            "CC62F6E8530B834190C351D63A76FF4C1729937362F73E3D60A9A61676B0FFFC3A5774C425415E2A039034B277EC3DBF",
            "174AA55C932A81D7BC01652BDB4677569B92351F3BAA631CFDC5D8293596902D3C743A8B94196B219652D671FD",
            "E4EEDD833536E4A4475A5682BC789449BA66B70BC66CB58C15E75B160E684C8DCB7EC826234D40EC9A0C13F99662F4CDE926999C7EDC653E83D86B10BE6BA06C38A6DDE9055D7ADC88972C8637D4458874E0BE964EA243BA2A128CB6525108B647124A70777BADB88254BA7DA0C1C4290C35C04B786897363D3FC6101A5482D84E393DD9CD27215660C9A0A50D1B0915CFBF329836AF37C617662B198BB2087FA37E673BD4BB6B9F153A2115EB78BC0990E4739B957D195F186B0D6C919FE9F02E6CD49E3D5D2775CEA6C0968EB131BD92E03DFB9BE9E13B3624B6329F9E8999714DFFF021DE019489581251A5D3EEFC4C46A5A7D91F71D86C3A7E1AD9473830",
            "B69D3985CD89B8C48099B365ABD2906494392331BFBC8CAB687ECC3BBB98F29FA6D9204BB02E68C0785C437A7C5E6C5B56E0C12FBA36176A57C14ECEE61E12D5F8E9BB4FEBC750DCED3EAE32B1804EBECA7996B4E9089A71EA521D1933DB851444BB775EA849D3FE5C15B50DB2352E8E58FB09EE86D6AB246F4CFA1DCA03443585CA5EBCDE7B1DF59BB12C5261A2CF0B1DFF443A1F6428E3A4A26D45F81434E411CB80133D750FFC7C6C79157618E87F01EB0F5C3280676DF828B051563ABA66144615A216F1A885BCE58DE0B2C5D3DAAA22983E9D1B490D4156EF8BCD2D3F2BA04CAD85D6CFA7CEC717F414339856D04DE58A4843F4F9FE4F63F977D6B313D7",
            "E4EEDD833536E4A4475A5682BC789449BA66B70BC66CB58C15E75B160E684C8DCB7EC826234D40EC9A0C13F99662F4CDE926999C7EDC653E83D86B10BE6BA06C38A6DDE9055D7ADC88972C8637D4458874E0BE964EA243BA2A128CB6525108B647124A70777BADB88254BA7DA0C1C4290C35C04B786897363D3FC6101A5482D84E393DD9CD27215660C9A0A50D1B0915CFBF329836AF37C617662B198BB2087FA37E7C0329433FCD577894D4A3F642836999A751D8AFAB343B50EFD9C345CD7B003EE5481F57D740B98C85A10ED25F4B88F3B21186F7B2F666DE3C016233F73CC0A6085D2AD55C7979556A111AD7466D556129B37B9A26AEC85C846E53A78D4F"
        )]
        public void ShouldCorrectlyGeneratePrimesFips186_5(int nlen, int bitlen1, int bitlen2, int bitlen3, int bitlen4, string e, string xp1, string xp2, string xp, string xq1, string xq2, string xq, string p, string q)
        {
            var param = new PrimeGeneratorParameters
            {
                Modulus = nlen,
                PublicE = new BitString(e).ToPositiveBigInteger(),
                BitLens = new[] { bitlen1, bitlen2, bitlen3, bitlen4 }
            };

            var entropyProvider = new TestableEntropyProvider();
            entropyProvider.AddEntropy(new BitString(xp1, bitlen1));
            entropyProvider.AddEntropy(new BitString(xp2, bitlen2));
            entropyProvider.AddEntropy(new BitString(xp).ToPositiveBigInteger());
            entropyProvider.AddEntropy(new BitString(xq1, bitlen3));
            entropyProvider.AddEntropy(new BitString(xq2, bitlen4));
            entropyProvider.AddEntropy(new BitString(xq).ToPositiveBigInteger());

            var subject = new AllProbablePrimesWithConditionsGenerator(entropyProvider, PrimeTestModes.TwoPow100ErrorBound);

            var result = subject.GeneratePrimesFips186_5(param);

            Assert.IsTrue(result.Success, result.ErrorMessage);
            Assert.AreEqual(p, new BitString(result.Primes.P).ToHex(), "p");
            Assert.AreEqual(q, new BitString(result.Primes.Q).ToHex(), "q");
        }
    }
}