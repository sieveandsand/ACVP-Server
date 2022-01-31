﻿using NIST.CVP.ACVTS.Libraries.Crypto.Common.Asymmetric.LMS.Enums;
using NIST.CVP.ACVTS.Libraries.Crypto.LMS;
using NIST.CVP.ACVTS.Libraries.Math;
using NIST.CVP.ACVTS.Tests.Core.TestCategoryAttributes;
using NUnit.Framework;
using System.Threading.Tasks;

namespace NIST.CVP.ACVTS.Libraries.Generation.LMS.SigGen.IntegrationTests
{
    [TestFixture, LongRunningIntegrationTest]
    public class MctTests
    {
        [Test]
        [TestCase(new LmsType[] { LmsType.LMS_SHA256_M32_H5 }, new LmotsType[] { LmotsType.LMOTS_SHA256_N32_W4 }, "7878787878787878787878787878787878787878787878787878787878787878", "1212121212121212121212121212121212121212121212121212121212121212", "adadadadadadadadadadadadadadadad", "000000000000000b00000003b85287f5169d0c1351ed2531aebbae537e9fb0c4e45593d7f7896ac183e74df92e1f1689fa94e0849f7f8d83ed2fe49752fa12b4a0882a254173ae553a788c5c78af942acee3be79d630c150785b39f1d3aac5a46e86c9ca836c72970218da6551130ad992016dbaa2d9a4dda61d6ecb010fcf9b5c537a3be6624398f32357f9bad9db60403a35518b6f4391c956000e26a75f2a6ca535a0f7d51491e8b620facc1658d8fa1b3f5dd87c398564affda2e5bf4da5c0b57de2db3b8679beb04d530240c38a9dafce6fe731b73cadd3488d07e1268806163094d285d431976df8c905c1f0437124a21cd65124a6900ba6dc41fc4878d1dbc8c27f3fa85ca5e8cd6556739994e61a158648735729dd5c534d32da005c2e24676cfa1709bc2ca45c13c22f189afcb90228fe96f98cff9275e0190569ba25db8208b7b5da7ffa98c89986f868667980cb25458c97332455fbd4775681a8b8b7b697e217fffc2a4b58c0678ca1dacd82e74859e6a7eb6046617de2fe8c17cadbb144a63dff19fa6a9a0edff7abaac8fc31ea7a720060bb528bcc555d6f8d1a53977392b656a70840882fe90d0fc3053271ed7f22e192b8b07a2f30b4da9da42ea20736d73ff03400a3d6ae9aa1fad9d883a643e7ae6847ff8daddd89e411251b9d6bba4151ade993fdf04977cce97e56f4ab7d7d50e939e0ff1daea79b3f30fd60b91674d25b15a9cf477077ad015bd619cf41ca5d542af19428bc83ab9642e6e8a5695e8f474e4d06d699a36ee0594347277a926fcc4b4cd2579c48e6d64230b8cfb7d701e1581dc6f8d3bc97d3dc08af1336245025c320b5e9a2080ffbeea3cc749218041d91937064b1007072c23e23a0229013d00b1334418317c1421341b5dbdd02b0f459bfe45c11516d4b073a2a113f30eae85c137a8329b5f1ec58576a66dcefa6dec15e4df4afe8cc26c403439eec6057abb4bbce9c4a7331f42ac2f733e6a4f6c82d8d3a4821962e95a2cd1ae9993769842d450e66d16c5537adf41fdf38b4ed44c91307135a047b22d4e1522b71c67278bc6e3f348cab56451efff994e3b6f69410672528f0db78d3e1075367607c0eb947667ee256251b61d634d95bf3b50e247f818a70853650cbfae592657a3a77f9b7cfaf7d9e6c05faecb05c5325ec7c43d8ee06844b9c32cfe0c70b509c40d4307b694843e41d8a3f2cae7074b831d8071fc4b5c7c8ce2fd59b4c0210fe8335b36f87da5948b2809b619d8a3eb4235d852791cddb3c6094ad18513c821b2eeec3e4f917cf9dce9ca775a1b91523ffa623e72d2e137361e597a46ae6c3e6bdb08c52fed0cb8d76695ce21a003fd0a772cbc02fdbcaa39bbe64a324f11be9f1eb3d9c191076cec9fc0dfdd3d588b680440b29cca023a29244306551f6f90cf0b809fdcffb0fe738bd93fa7d3a4ea0b1b4facdbeea57b2e7f9a514edc645c7d4649bd49ca3cf1511c37728c7d4a559df986d398c34ac8ad46ce11e234dc90318215e670725b14247bcd5eb0a553a4043c94725bfc8395761de419069f64a0da2242000203d19b328d41c5d8aefcaedf8f0f55da086de5f53e65012976bfac3ae013dd680463f77ea081926e5582a39922c8265044450ba40d4242d71f6817d19b37dffc6ac55fecbcc71483f789869d85f50da36710f348f4d9d3ef923cb9352c8cad7af4bd4b173a188512cdf4b67f788fc1407b801dda25302a76df8c5d911868d41da940330fd99d8e9bffc10b45d2b24b091aae592a84f612fcc17dfc1dcbc4897a0fd2a63d6ddda5f721931405a070766ac41eb067677ff147ee2b4fe54961cf9ad935f38bcb76039226bc2e889ec762f60c0087e6d40d6aa46b79801ce20b74a0751749b52234dbe6b9db226a59cef8496ac0e2c5f64fd599f29d5a0263e8b97814d51fa2532d0da8d6e8839b8ccfb674e0b30a06c771649a7f4097a1514e2fd6da4b72f22317bf23e921873c2f1ba72116e3f8de485540a889597caef9693df4a41d7371cc5622ad7a886d2c81e146cec4f6ef0d4c75f2a6e8b6edf2ac077c7b13e7457653e8d724c5c472ba8761255c83610ee70e42f28eb90e5ebbfb95efa6a808b78457303faea845414016c2f59f30e808702e1974de3835185191d45396778f496650271552cd8bc7b205007e76835e4925bfd8f200a83cc8244855e00372ba09517706025741190be34f1091c96950e5142e0dce9188355ac90e43c13340a04f5aa301f0d04cd8f0f1b6793e88626a693795d1025b552f9c63a978e63c012d327185b8c071e449c9607ae995a1a90035ba0691e1f9ca767fee303f50f1cb0059d8ad35d867f030addd83f2b8ee528f942b9842c804b41af881fcd9f1662764694d14aeea4e0225a29f24fe9294b8e235f9983c8d0ee60a76504b4c5d4a9668ed8834a833b36c332510eea1c4691712a3ccf8edcda70cb5598f5103fb3880db0b0f6551a47d35c5f4fdde31e07b54697645981a48f956ce36dae96b0688e0535addece9773ba1d4201a845471f5dafb523a4ff6a4323ff224987fde3132b8fc47918e78fc9840d52dd0ee1d6cad28ddf44fbf4d01b972981aea8595f3b24d3b210e8735a9eb4c9671489cdeb98d8ccb4448bc642d76aad03af943ab05cdce1cd4757dea4fe12489addb6626d8fa574206b2de3041fe56446a13ea5bda684d117307b6bfc6302dfe3d82e0c5f39e4119f3620408f828ba8c2ba08ae9c84100d0338c0d356e653eafa2665f7fb29c29933720049406e6ab4d5b0a31619979642564f957b8ba2bf1ba37c189089cc636bf220c961cb3db4d60cc6efec9ef95fdba198fb10a39ab1bc15f4e89971cf20cde08d6b2e3dbc0703a26628b00560df05600879c1d8c7bc8adbf92b35636efda229b0a5b0eaaf7bc93454616c68384097d618104233ffd2b3dade1fd0707468e562f27c7fb5d94bbbe98191f609e9a975c7ea8313cc0556ecb18f6e25158b043a50b2eadc1be5ab7791b0b15df074a3f4aed81ab1a6adb39afe020b48a43283411191ef1d811d0104fc4428fb3bf015db0e2e65767e7e91a50635ce1ac1903020c200000005a7f69b19b51bd3142db3dd7868f1cbce51ef550fca184351690099d2bb676ef90e12252783dc8aa5f110496a17a1354c766e5e7c04e94af3d557bf6270e938b32e6d537d5eafc0e736146b0ddc124fb9b14a4c13e99ab022b4c88b93d3f5eea94abf408edb7ac146795d20c76b5c46886348b37fcd4f9d87e0fa6a85d4726d45232153168f881835392958c8f650a9fd6eeef1964e0331841af0af76add241d3")]
        [TestCase(new LmsType[] { LmsType.LMS_SHA256_M32_H10, LmsType.LMS_SHA256_M32_H5 }, new LmotsType[] { LmotsType.LMOTS_SHA256_N32_W4, LmotsType.LMOTS_SHA256_N32_W2 }, "1212121212121212121212121212121212121212121212121212121212121212", "7878787878787878787878787878787878787878787878787878787878787878", "babababababababababababababababa", "0000000100000009000000031758d7b95141df14f742325a0e182ad14699e284ebd7683695d3db5e48f4ee33d66f08f6fded8b2bfb536ccd6bfbf33052534053c4032b1036ec1768f46fbc259bf5482614fa750bb5e1eb1eaaeca0d8f7f24c292ab1e03c60195e6aa4d8440612897769d27e91278768671351902b77cba9b7903bd7ac64196b1d3605e41e56ebce74731e5b23b6036e94d1a01c12383084f3503eb0dc9b3d48850cc241b120fdda2a9d46aafb49969e02bb5124ed5655485844296d7a7151ad42c2a5ec79932f273b8c186e4b40742f7e8a8565818f4874a9e30b3b41722d6991e1e0e63961458e1d6788cc87f0c639144ccc514f04ae0e4488107e1b7999f8bc85bb964d1464a13215b5832ffd7aa05a520b622aa47a5d2d8093d1346f824b2965dc7ada03279f565887d612681a7aa9c8bb9e995aa19f8b1d2382c7ecd7a8d994069e3f6f5e5b50e18230d5a701502a54427cb1ae6deb9628b45588fb77c70bc4ff158d967ba844b69888ac2cb74a1f7dda70e0d8b9c911baf0cfd56b9e64fe33be8e16af7446a7d9e2b0f1235734703ddce70ecfbd8001155a572d9a3dbb485ec4a1fe581f933de5c63ce4182c48649c3731cefbdc4d25c4bd89d08002819412c628d5e67c402964684b6e9166795297a8ef465eeabb5bda1bc48aab3ecd99abf9c38219ede628b9f47dfc6c1eee8c346ea7697b181250f0dae7907fa0e08aa201014e5cb80b3412e3e4594b3628916f2a8573c42499cd208aa0777a24c101e69a935ec4772f79cfda043fb7e5bfe9ec2a184761affa11055a2f5cc7b062f38eaa6244a48982f0d9aec9ae1a5fbb8a23061a9d5667c282134e6c70fb9107f3d36a582f94469e0fffd61edb0ecbddfb6da7c943bf9cf79de04f4025bc4797fe5dcaaccf4b3cb673464fa5bc84a5c07ae98ae96413d5fbbcf7d516b139edd2bbd31486664c0d830ad54d68b85da3f907a6e42e5c8e7a36418c43cfcbc2121cf0195b9368ac0c80f4bd608c8dfb89937bb8741719ad3cc66b00d6c37e52ff81c73241ed68d5b6ef14d93fe0c1889e515f964184a1e4198f49629a6c31f89b2fb3ce5f72bfedb73ff1a84f45a836ba67856399402f9d27677f6a81c040cc37ed9cd7414a60924338bdacb0ca1ed88f9807d974424942a5ee6a24eb9ef8a10ead529519d44f542086ef9eacf5b0dfb5aee7ea1585b9fc89ba8171fc1202d09b312e688d9bfdc82a8843feb84bea644ac0d94e4f87c0aa6a9c5da6efe4a5236417c52f50b80ace05365948c336f630593e96c3768f767a3084d58a4ceb18e517604834c12edb02060e7ec838373f1d89eded3cd6e75090fe6b6db7733c47b21febdd8fb19cc8ce635b0a056eff39495df7796148121d3ec343f99909437b31166e50547dbadfed6b5829fbe67d93aa291f5bd8b390a02ab5400021c6cb69f7cce0b3bc49f1046d49280d48a22229dcc910d03b733a0dd82b97d55a7752eb17e6b3f588982e6271ce54e7b49e7565901d6522f28701f61918a54817a900ea6d6e7f72914381606270c72f76bee39ef89f7e86682e1ef3d3f3f825e1e74de08251aabc861c940c7d7879e097b28952fa55afbae62c09a24d79882ea6598dd79d05be6f51ae7a3fe8f193d75d1303334706466f9a0092299a0f04bf62aadc6f9f8a8a2392b5507b3dc9273d48b340198c11c23d8e0d37ea2cd79b0ba0416cbaef86f17785ecdcff67b79575c6e949482ff286b22161a7ffac95631345a7f38e34db84515fb09aeb36e5a36bce619f356f7b2e91efe5a4c019e19c3261e8dd259ebaf841097e640d38bb371add078a73e8e2f1740378b22bedf0a55ccb5c175917b463aab8171d19a4127ac2748d051f7f909b36699bcc41c1e5eb2a9d881c80027a82b173b54804b60f99b00adc887975d322e2d16603d96de36b1e706ba856d9822f47bfc8ec8146a29b89e757ecb7e6e936a24437c033f2ac3947412c42e32560f69f620a3c911e89ff52c535b2d4b79c5758bdbd93e7cd72871ff5aef5bbca74a16519501057769153cde3c389f5b3cf1e53374956bc24839d66fffed41ef1c1f95569f9b15fe3520e67200a8531d2ca836371403440cf77be1b3c5aa378c63572cf450c6fb23e5606dd4c72688f8e56be52451e36d2d5e42f698a1ce29c42ad2a60d569adb4781645d667000ba05df755ecbf72a200a27206f62fadc82c2e91b5d7a98c8b68838f895610289f85388f05aac6376630b2a8a4f7ea1b2bbb531de34aafbfaba36a7fa1ae64efdf7fb242b7024bd587af7448bc02294a62ad60e9b3523786ddbdc2e065132824406b5e7e0b582df0eb9ecd151d7fb585aacd7416c8c8e0315972994ce6b92e33ed97ebae41ab181be2a776c556bae1e308a92154067a313bd327687b630c7f264837d8c1a243adcb3e435ad33619b581f2e440bc657047bf19bcd3fbb5d7a09437a767c6c7cb88c73a1619c301ae05e3dd1402f0aa1137f46f675a2236877acbe95c89117dc7dab286765cfa8e81d807207368a49a3e5b9317e195cedf429dcfe6108b1ce680b5d8c6ea3ddcf49da6a221af45e69314e7638267adfb68f2898132d16d139b4dea3da177cf6bcef054fb72f26b59cc3544ab19e7c37539409b64947b75d8db685dff37b516945b22fc845dd1f8fd43e11235c4a52dbde07c6e9bfbcde244a02ede088556fe8a6bc1beb1e1765af49452934894677ebef6f7a2963f5105b50f930ec4888111c19e9ad4653ab92c59a703b3b9edd52773422419755e642e354d6e51331da3cfc136895105981f92234602ab588c7f8471f5a27d00c2f4609baaec276515f1b22bd350d0dce6f9eaf1f47dad2b25fab682725712e7681126fb5025fed7b736d57daf55a9b06c404f0a12e4897c2804c9df6fa0b82b064e93b8fc9db170abb4ca872247b164e291827625e03e0b9102b855fc45bbd08f05ab2dc99b9c9f1ea8c90d5a1949f0993d3a320634cd5e895bba28faa537d4bfd5b0f9a0bd5a5014c39cfc2a275a89e60b7c9d4dcc5e0858ae79326a6f2cfd7eefcf18a878c50df129b51b50273129876366fd45577564cc7f13000000068c1db0125afcd10d5d2a6c24ec8f8d29fdd43c2ea06d8852e1f7598712ce5bebf6f7642c183b722a5b8abeea542d402699e39eb04bae54c71589289720b78afbde1dd3d5b3aa63ca04840b55e09b866c1f7ec547c61bf0ab10f6bf4fb4a8de61e675670af90d594d7fcaf1ed9ac2cc336ea8674b528b63dd682f899c787245db1a4757fb1da4f08274d35651df397feb20acf2f506b3a7c5f2c2948aece26804d64ad40e72e70ee5352d1dd6ceb79326f3b12166196b9d0d130400cace90fd87f7131b40742e1e6c598be0d7c23c2a525549843dbe38588948ad6aaa5c847ad7ee3fb461fd9e6047d83d960c5942cf6ded050b83688e7568f01c47444e95569b03b9087bcd94741d89c32ce6335d55cf0454f231101b6bcd8ef4e3c8f10b449d784c3167f467328bc4a5c120d7ec5b100d4fba13f245d3c9886235bd4e29b62400000005000000026725fbc66aff5a9cd2b3cff7a8d7622d1aa9d5c80aacc29c3247f5ad4634ba7fcae6d081d3f3f1c93090e12df4a327770000000b000000021f0beca34f2418dc21ebabc18c1283ff5e126e3a18bc0ae887242fa1db6d5068b1799adae9141ab78c67bbac7fe8e045826194b18500f83f36750a3ec3057b1182a4616cf05fc33fec7ef47622d4622ece9e2306c002e1de2e98df83b0e57df72021cafbca054309349236e1205a57bd976a20d00b7c8dc3917fb2bbf1944d53160fe82bafb27e0dc7b754c037105bf93c6f38818ea8da12b8510263546fc4c8e6cc515e7ac4dea32ebfe71f102af83545ce21cbd5ce06da498dfec70c921d6c022ac518fe0626a69e4636d14d1cc2532cdb1e97200dc47b8af223c1e33e3eb1addbe6dd3458704a086d133344611e66912073d949d48f64fda772e61a4f1dec8bfb1743c04928b421db279ef582251d6f9bb6ea27e1a9bcbf7247f9ebfc1c7c29c3a2730f55e488233aecc869151e561cfe7930f75d7467a48afe508a6b78d1e3ac36cb0ad2dbede193540c0cff8ce6f331650921b1777e5396fa37e41070b0c7ca4835ab7179d0496e232d44738c46aa12e4415405ceb09023841136d30ce952e0819c7c3a9c6e0263033d0bd9b545767b31442be79f053d0befa251443949af32e6fc30a170c90a934d404ecb40cda0b820f41365a1a479eb85b90ec6bfe326d15eff29a71128a2ceb7855fdfa67f380325a035bc9f4bcbe45b61e6153761cc5a00e5e8e25ce7ac48e8189fa3f574ef8d901d64f28e609e494930124d6179d8225e80379615dbe34f2627c9dc5198420c5433a1de0ce1d56a6e18e17dd4ab2ab07fef866556198dbe0db6f4f0da30d24188bcc8cd223335797e5bcb487ebb0d0ac9ee13d603f15dafd92f7d3c8ee787f79c94a4830f780305430c168353f72a70f36be514280f0e3a6c158b25e092c75e9a3329cfd948b55037ae0e35c0085e1c1bbe787fc4c4b2c0ee3b20d012e536db734cfe8ed15cfb5ff4bca243adf86f86411ebb382f40e124e27f0432ba3e3c05b43d96c47dfaace153a4176b910324fc08a76f479f83f55c3b50738121fef520c54e8ec89ed4fb5f3942e9494ae8424624a591cc1c27c78efabdb7e27b930d347577fe10eb1c3f0d32159607cdbd4bd970c6f1e6f55cdf73a472326d9425ceb72f999656b231d19d92423d0baf214f1a25e71a7d5883f93c0a70792c010138ae41f842d53178c4252cba06b9891d08f12a6d13bacb427ba5e37e6cb3d0907d5e1b5e34f7e51034452d09d5ccc4af665dee920ede7fe4d8af45b294b3c52b92dd70db1bec4889d815cab4971df3df6373dcdb581b808eb9ba68343b694c882b6d2b833b3dd7cb5e6bc8515cd50fb0e3ecac86dea210b1e001e9c2faf05fade8dc01ee755b6657619a5914f806e725a3ad991e7660ea500fc6faf4fc24b79fd456aa2764959e074dc7443f9d24b4ccc6b9e3c7ab14027a49573b7f37154ca7c3b36e54dbdf7f5fdcf934748aeff3811d742f321dcdf1a261442847d21f334aac0b6f8b98d596d267ec6607f0f4e44fc5ff36f0916b276c4e3044d910a5a66d91178b070e8da9534a5b6902626e4d38820610ffb86cd4f52a668816c17f7afb694f1fcfc8102456faf4fde6cc5da77c2f733a64cc16fbc4fc1255cd8c9da863417e1dd4a433093493074b3eeeffd97c3d814e5273fe55817c6d6246f6cfe53663a1f673af9d38aa16918cb719dc6da26d6406237ee44dd1e29fa8b9304272d3c1d088d0c7664cd48ae2e59a320629378ba81608383476ab1f4436bcc387b5b9cc52a1ca1477b6445123201b364a40fd074296b555beae7f717c1d4c1422989d7084362c86bd4fd1fcdde16e397661b1aa63697e33792cee0292a9813043944bcff15e1cc1cd0e79ff7f9851d8f1d23b14b29a252f347eabd05a6a98e107850d62a80ebe71a0246179264ae48a00596cf5d9d0c2a113d1054d8811af5e6a692d69015b7f1f7b5a4abe58f6afa84373a90183572dc75e1fdb8c877d2d2ddbc583fa4e1480271da44dc96f7f22dae83c5adfd2538354f98e0f4dc929589d8852ee502ec4ae08df1d46fcd9372cd85625b674fd37035e9235e336523c5ee6287a6add5fee2f43d007f0e6fc629c2b813e7a6bcdf57853c089b7d37ec8594840a2516c3d4902623241d60063c615d241c3875ab26e114923366ecca842d31ff6999a40c9c6222f23cb8a4c2eb63f88e1f27e00c82912a030c01a14a445db9295e7abfe0623e5009c761d8a85691640187090b65e648ef8e5d705788a0b7e34c5799ecdecd5513edd391d35bff35866b6fd6da67df5782455d81002d4371cce300ac1b61ffa799ddf63ec1d93b605801d917247ec8e868b106e44d79b87c0c0422a43513d6832da1ffa52372b729e02853b47da3e96ed64a707762875375c495063c71aeaae2329c2a1f2dd4636285823bf82f5ab474001a17a65dfd233602ee4fa14b21e16ae7629decc4c908ca2f62e796c808e8db4d975670babfb18fd0446ef504d6012498c7f503d0ad3a735ecf2c9d3f75acbe80e803e5440b1f3afe14def2e04f90501234123a86855989ec5493d77e158dd8b6ee4e9101c73580b037bb693e82c20e09bb0b5019731cee1384b66d413f6dc375699a5ba46a57972144c9502b5d4ac2c6327c6b4e5d683c4e07604c686f4f164d677c93d85978e08b8371dbf97ba5613f738f9ed3316a118ab998bec169cab06f9cbc3068383efc46a99916b8d793aa7fe06717310e1881de984f1cee81ec7f3a9a2a751000ea6c71d2adc6d2b90678805f12458157a1be6d26667c337e1753ebeaf52d01e33ee08d9094247a26cccec1a0ff4fe58b56f01f871f90221f15ca564db4b0f313e011913f89034f6561eaa0d75944a9812eedb3c8c396fff64b8677d525c0a51604f4df3e6ca234a36c19fb76e0b298ceb03c61a40db4917dc71a603609d59a032f8dcfcb09a96b16df510b035be19f82ea66d58817b07f735ce24f6758e78be3b27260561148b4772e2d44a7e1f721b1a56265ae030623639129c53bd15a5390f05dd71e84ed5c59ec039b291fbd830e3b62b6b0783cb4169de86e292a740a77edcc796e4bfaec31e07dce092dc86191a73c01734444930296aecc7bc661a9dbae70b1b64d87ddd6874ab003626c555fc3d475b14ff7f308fac1c306d326bafa57ba4eb81cd49eb6c2a7e8ce4313d38a6dc25e5567ee895b5ab5394b3c9d763f9f96589928151cbd5b9d5a6806e1e900bd0e6096df10208ff69543275d573fa403fc778d73695818c57797189c2bfe3abec23d3a0a69d6feaf56c448da6f2832b12d782e8bf0d4b4217d102c1dc933993dad133a7a94c8a21e653e55e13538b6b063c610892cd531661413602bf565c57ff66cc0df846f1238efa5947f65ed3d0921086245fc373295d5fa277629807dd2754956d52acc2eadcae85252115889482c56d91a90c2a3a1e8fbda34fa417cb3144cd98c66a59f33a1bde63d0c03687ea45dcbddcddf06b9845ff0e1ea080d11dd03a98b66de08cae7e091e383087fcaf071e410edcf20a2e556ac3ac2ca8da44f50659b1be709ebe414bea3b7197863d31b67a4485d2da988487ba83f92a9db36ca09d76e47cc61c5c014ca26b2b72f1cd9bdf9a36b58c6f23d6b6b50d9e6248fcd5efdb518f21a377aff0d83563a8b307270a5c9c9a9c38852b8d8dc2d345b4478c14a120e3c1f52ff6892b88ad8d9260d4a859ef580da200eb2182aa3fbe421784a5819127fd116b6baa8f785161b715ba2d11771e5700c9261854b3fa8de31e7e43ed8a632ae974302328a5fd348f5e8aece2e1aa95164e345b8330854608d88d4b20aad078fc2195db21b345d70e4b9415c34492ef4fe2b8a1ff4aa26a5b45b88c5a08752bc0f1d199edf9abfaa75fd80a57d8669e0b683f414ed6fea1ce2c265cb33dab5f5aa94b6807653c71c92896984f62476d0c86ef84d466d6b8bccaaf2207111ab90c89158859085c183a1dc2cb83245c6574e208c3c01a8e8bb00263e3ff7f651202d5cfb845f2d83d7cd6b84ee2a07293f011173d6610a5de7d398f8c437aeef74a3934f029b04115b0842d5267436a0178f5f26f74f567af56f7112d5ca054f295781f81bc2be5127679ea6426675af697ffd69bfd47323ea9647199f588727901df76cf1529cfbf9d04d7a75306d54489c4eeb9d662412099bd3e2f145f9e331a3c2a95dff98adf7ac9d824eb8ef11a0ed9c022ea08bcb5687bad7bc52de84cf14ec0add9b08882378f3933c030cc3d55c90fb44674f2577ded43e8b8de8baab4c1ceee5964c83f40a4a1eeab466ec09a94ee7ae9573c35ff374436e27ee18ace5e27fef8b0a7717949f2e761dc08e1f5a6ad71e7f96404b71d2a08a2b3b4fdc2716b39b7f696544abaa9c14da6f7b7b9a66d6c3aece77c367d69d0a390838d146500b18b5f88804574b1cf30dd70e610564dc6c650883eaf11fdab597169f0b9b52c7ed9ec6a648618622eba3acb8f1c3f40bc3a010fff5c1b9a52676704178d1890d7344125d9be082bfe61601afa51b5623754c4beb48b1217ce73a34d3debff212a5e0b0960790b9d6944a798360b6368137b534f81b698ba43c1e3d95427cc0ef7d2014d00ad82715845a9d1d5e99508ee2bc66ff7eb401266fcf06444653fb7e475ff4d7b7bbe5313088eadf659dab2fca11c6c2cc44c5e3a2d109f102bba18cd8429a2c722ff13466a62d0e20cd889f1de6fffface977899ab168c7387d662aa429768916d505248e5f48e283f5865ceffe609346dcf7ac09f1ddb646f332e12090a5bb9531fd776c4d9affbd2e39cfa239eac16a7e37d3a500eed5d7917a6307f429843d8c8fc9f0d43ae63018d813285f25f9047a832b52d09513d9e83895a3c0cda616062aa2f063eb1948ff3672a437109fc6933cfb3c77a6cdeccb4c536bde9584ce0c4b5c6cb8b5006725c5820523b39d3ec87f17b46aa92d4c475d13538968ede187b8773db88cb0837ead3bb6b240794d6d091690300f4d7bf4598227c6d67b632fd9239f598d15916b8f1855dfd772c8d0c87af1cfc6a75f66410906c85eead317bfa965006ceacfc4340acbdd6c35e2cc89f8b9eba64d353773d6769bd3c42ac93fdf9f9c663801fd9964b53be50c1bebc943b90439793c7a95ef584b63ce7cfe5f410bca9b120fb28e3aada080f8733df4ac3e26726f08b987263eadd4c2a95124b017b9af53862e5ddedf4bf6b6d9aaed77535104e6e55783709d04d8a48065452394cf0c2063ab98839a311a676fbc4df8d77c1e7e2ec8ea06573f046568ff7baac047d9759b9e107f8dd7d27b2c4e47d44842a4cb35fbe90736ec1d1b0f6abf59e25446bd07e7d92cd2616d85a2bbea8a6ef2c0d3328e17c5e33b746399c348afe8a04f191effe1baf9b1c5b8de10a16d3c497b1e6c694a5b9e3704c9189746c3c7a351a7e6656cc6c3b4f6a049557c1f21a2e7c31f304498946238ae4b8b5e5c7270933f2cc6502b6fce35c82598e3e4679a5ac2f1d7115b1e4dda45d7e7746f86a4419d4dbad3581d717532578ae24df68142529f5ec8a5a59f45db485fe565b30df954eabf753f66a388ece50e2e30f855a253f78a474982559a4fa3d2fbc1b8ed5c46a3510b9bb7ed4ae19af5ec40fec679e6e14de4034ab9d481c1081b6126ab5709e81908624b80bceaed3d9084fccc6c65374dab45f12ac762d9e12ca271d522f2fc72b29ec337120fd5b2ef7fa40cda3322728d19d5eb47ee48090469967f98db3f37a4803d55cf7d255bf7e25d880d4964d49c2cca494e377cfa13770d0f2efd885685b80cfbb2394ed90aa108985fd1b4a26bd6615344245064151467f477c86094aa1ed79dfb9c833c16233fbcd2ca1db5c46e223af7ea5fd47be9f7179096ecbbb2401d158085dd4d92b7b056f0318d1b703bbe4c3ca1edfb8eab99fbc7fcd7dd1bed8a804513b8f5e516a5b40ea2055640a0f9c047092ade47b59432963e1eb8bedfc7bba3e311a60cb57dec3d275b0b3b883a6e5d3aad1851a41abd7a93412bb418fd68a8e988c3bbe1f9b2d88513093901590f99915c3db25d6ea19c38e9dfc030a2d783aab76d5765eb85800000005a1db0a8dcd096d1184c0d746fa4397a60d8a3fc79a2d9429901c2e73ac0719caa00501601753ba8d1cc004260288ea3e6e1b233f4dc85c9c2bc10698c450ab12c22bc2615e2310672b308afdc83085c111cd86f555abbdac74c0251a12bd87b88657ca09c850440da870bea7ddedca52445668596f711d798767ffd4651e54d47773409475cc526202dac07b2c11ba9dadc84ad87886e560fbe27d05270904b1")]
        [TestCase(new LmsType[] { LmsType.LMS_SHA256_M32_H5, LmsType.LMS_SHA256_M32_H5, LmsType.LMS_SHA256_M32_H5 }, new LmotsType[] { LmotsType.LMOTS_SHA256_N32_W4, LmotsType.LMOTS_SHA256_N32_W4, LmotsType.LMOTS_SHA256_N32_W4 }, "7000000000000000000000000000000000000000000000000000000000000008", "1000000000000000000000000000000000000000000000000000000000000002", "a000000000000000000000000000000d", "000000020000000000000003da487ec28ade4f01323a3778f2353642a6da7248611d2578d196dae4b4ba0892949b0def9579e3c060d3e5b44431186e6cddb1dce576d604a172fea4f6c10ec9ec3f8b3d2ab954a7ffca48a468021e338d1782e3cba1c627da221df17ef6d67aa572e7ef6fbc8f5887f07159b647b3f5d1fc8f2216c18786f2aa719f1a7616d0154be53bcbab5a7361146c43e79c02925c0c5d8b3626ec7aea6fc2ced8536121d278085c7c1bc7553e91932b3c48f5278c0dcb59e1f4c23a89afff3b42565a0adb0979c51d3d033273affaec5f5f5d14fe5d0bb424f0138769b69b8db6bb536dfbcba753dd424b05e0cc756f453931ddac37f2f19235e476ffaf1026166e6a35b2bddb6c5fa0aa42b70c5684d93859031f92f81152d66b6e3eb617d2257d043d70e66b3f5c6088127b140793705f5c9f83c7c7545530dda5254d999980f33ac82636b5e4955fa0c1aaf17399d8ff38d1914cb78980223dc7bc218ddec65b5ad5e886374e5aa0dcaec6aec88d7555b5333b562d9f192d502af0e614c1baccd90e186fbc58f689f34281c1bb09a12d4af63580938b6dada74eb8744f904afbf881c05476b417b2e20c4cc6032e07bcf38c46292c735faafb63bc8c1c66c2e6ca42a8012b0ac0e3f18428b7a29d5221163d283cbcb08292315573e3de021da01f99458052f38ba3619fd0aef54d643f7aa01bf29119b2fda17a8419edcc94c4b2bc4a89477ad45730bae1f4eec0d8188ec0b440258dee4738b2f45afcec37b55208103d6194ef3e8af85a7f5d76629c83713eb7540853c4fc1240ae30db4bd873ee911364eac668cd493227342e807cd326affdf085b94e5098da91fc2d88fad81b01c63e0cd0896e0849161633eea0db6b3765f22a2657acde7eee3e3ea408448f90e5d99db4d3d910cce8f8e5fb96a7164402030c7e37db040341f6e6ad0d2756f6c8aca7ccea4e01da5d970a29955c9b9d257e80a19bc11e36c571d2ce16734c24402aa0d57fa9452799fc8ddfa3305f9bb5d84199a5037e0a1db6f7bda0904397237c4f63485d9315581d286b2c6ad166d3951012894c9a4096f2af7cc10c176479bacfa193b4af95bdb179ba1f92f4380a8c71247f84d7220b001346a9bfb8d066c418effb1e1c057b6b65e01b972241c20add0371454bdea7a1679f85d692dffbf0b6cf80a0986a159d7dc17110f01ac91685bb832fef848509766d413b2c6d8d1e4e966a9d126f526ddac2533d0c7728758184b78167a1d003610fb0f0cfc77ba63aaea560a135c6c2f0bc2cc7811ccde5e9e306f5142e28a4ffd030eabf6541221892f0e25631ef6c6ee6269dc6fb83e51bf8c7a3e02680206fdb5d4394fd2c475b8e4c70a01371535d032cc6f7270adb1636ee4b2a2b93a2491e15b8d94a91720c451af3d0200ef808832bc1c722a4bba9397fa51b730a11682cc753fc52115f6c5ffa68150d6269b087c7c3e656c8338421e572454874ea8617fb308fadd35de334e1463261d17e37311132dadc0b86515f08d5ba3841000d442d7eab68c1a49fa7c40f4c88582afda7a555d84215a43b0be27d1a74b3a1f61c44314a6b78ae49506f9ca386ce3d3380e61ac56a0702c07f10568f40c3abf5416693959fee3e9bf38b33621094e28bbb9115afb1f19223590f53e409f870aeecd4b8b2f6c5d7a988968f94a34cd9256d14309665084919fc8da0e7ee4ae87c2fba3d770736959c06890499bbaf9bd5a4662df55c9c7d9da6c6ed71c6e41776a5296b9aea9acfaa74ddef623b8ace2155eb0e2362d3355f2f83db3fbf3892824c7e55ca25ed23ea554ea1019693bf62f4ac8f294439e938f2d202462d6bce1cf5404e50cf78575f276dbdbe44868eb3117d937a83454d052f8d3a27cf164fd33571da5a2e974fe9dcc28da3ccd00a9a544ef51fc7dcd794f29e75feed34436806cb96848dc2f8fc191fdc3001ac455efb4cecc7e8aa52ee01607fb7287ca1f01cf67b4618ca03fad5be36fa53de21e00fa29a92873b80df8ff45eeaa141139dac6e9f40dc9c05ff272c875f7177a1e728dd5ba3c7d179d95c3773a1803d951d93f93f4790b5ba80f3435ae43a4a2dca320772028f046e72eaf0104309d3e483b63f381024e2e0e7364b317599556bc49589a3a4883109aa0985b089057288601e2ed808f6aac7c390a64d22146a37cb78649e4fb5a24612930503cc6ebc2a521c01749c93052247b31675b2390f77a69f247520afea3cc3d077151509ca663ebe73b2262491abca72622f23fc671c4fa70cd010fd701171c0926f3a53a6f6460e4af10f31d58cf07d80e93b175606ef580f342553126e4bd3f0a617049e552e2f176e5adafce0febe9426465e4dfee2d5d486b8880df7f4df01416445012890b3f18007feb37da82a71f273204ff840a0a14e6ecf58b4834b643175cd80b20a1ed23c3190e6ac02a0b28e31b1f77ec272ac3d5b1f6c39c10c7e51c229adf5d5679654c188515ce1bc9876dc583eb3acb89d171cb57f320eef4373131a01bfcb008f28e52fef1c67d3b73dd8d02279ea6f0e7242294e92a8eafb6d1517a27a81156bc9ee289da3f5e331ed07966606c58e17afe053f26a732d459dc2a9bae5783f7dabdc092d87ee59faaf7791e23b6092753608464a43f5d69462397dd8f45a6586a41b0bdf2448897244ab538c1e21655d3fa7ef7586e35014584011d43530761e128396a5125f28aaa72a82ddc82749a267c94128e4629e6bd4646f080cd6bb7827129b19d28e7f6a42f04e6de5e28e2e6ef3cb6bd98b965b44b5f1870ba7b7909e5878b306ffc3a6d5c2c62bf82398b7f317e0ca6037645690061d768a820299a5872ad88f985b7a4120a42208fda3f9c9fbfecaeed6014412d1bc028e73c50214a604e031902f976dd0cd2ef551ef6922ae5ef0c8f0513cc3bb73d16baa340979f3dab15dacae50404d198edd88ee15c99b54d2175c98a482500aef8250dc77092dc5a0ec0e9cd48e944c664928a102689bda282af9dd685d4b440b8570677ad7860e174d3e7ec93cfaae782b62b2706dd055eb4458ba5821ea0770c89a7e72041e00000005d8c367f5ce00382e298cc100d57240e662f4833f993800d6f0a01ddc34acf714adea65a99607c3ea747d4caf0e73e6760b5bd750fa3bb25ed485c1bb017a576824eadfb3712ef1b6afbb402652c7fd240bd72cede276163ed0303a881a9a7a01554c5e0e5418fb4ac2ecdcf9316597d0e39f2d8dd6a5809f6798f75843e5e75c995d344ba8e7c2ed2e3f883745afc498e7aa2d95b8a32d078011fba7d8aebfc8000000050000000312ff7ae83e51b73a02bfb4a9f9bf0a6df7b5bfb718981734cafb19a1ff138cc158adcf71743ea38a6e559a1bb3fcdf7d0000000900000003dd6c596d338aef6ae27eca4ad91e729c05a1e7f29ee1970760cb6ad7364c0b508cbdc90134c41131db10c18c93f7918108f8c4c3dfa1a86e9fb9fe158d01d9ffee5ca1fb586d9fdcd27039ddd1b7262297c3114aab1dcea09a5ca12afb9847132f6ef8978cf80be1d9564ac4a7feaa239c1644bcb7a498408cfa6f2cac0fc719caa981f4731a5e5f0a05c41728055fa17fb3b2a81fd862260e99571b552aa926f7884dcce438080cbca426b0063fa327848649951c4b3fbb0510a5a994214a79486fc005feca1e26ceb8097ff707eae3e18630329c2a8cda02f977aa9e0da3250672376c331ba8d0d3bfda9bb2c7a5393f74188dbd296671be46ada60b4f3af17f160ba5bc440065e69f767d0585b74f51fdbe8754069b8b34abc416eef4bef40eba12bf529ed3c3c88da0ebe7770a6d7885bb1bb54825819267eb9d753bc7f5cf16e28bf4530a653547306cbcc409f6be0dc5c9fa364bbad83653fb389b9f3fd2a4ab1c76aa14799fb16d36b10227716061dbb3be03214eef2216a962ddf84457a909e331f7c08736121923647478c4d0bc5ebdca97276c5b30c0035a174d685cb95997a3df57712c20d0a1d1c1872d9cd781689ce89d67d46baa4814a0956c6e0b20f1436e7983b45f2b3ce67e027b01a54d202dcca71caa9b6fac4e9440fbc49a4747700f3be37a96ea0a07f47612bc4379d8bcd11da88ebe3eb3060cdc932b1e9d526c36f58749bbfbea6eeac56254d47aa52eb7c81d709b1b9a6e8e5a63613897444f05671ad86b4eb093b76c9adf0f2bbc87f3afbc3e3f39a272f535ae0e288f277de7b2788c65dccae57da66b7f666a793db2c725e0ac348ecb50c0f980339df2a7e50b0e97aeb87f0217b179c473e29aec682b51d4547e2a8d6d777655b64b3b317798d5aef170cba5d392564fe34bf5313d32469f5efe4eaf3e1f127b023efb074723ac8ad5b2893a2be25ad93714faae787e10039ccb4334e3ab5abc6adfad78426e3350d9890a5924f89b7bc87866f62f0b4b2b68af6e1ecdf6f70f4f7b08acbca8cca9ac4c7c05b069b38ad2563d4fe977f7be3f40401f670bd8561c9c59a3ff49d35d1041164ac757cce52a95269f75e458987fa4531c1d5f0c329a9ed4528ee895a10a12302d53b80f52228ee6b5d14eb1d0cab9524808fccc122a77bc70fb7b02fc032f4bdf16cca1b66339eb048add8aeaaadfd8f74ea4499bb0f342e8f316c667afb4f1f6adcb8bad9e8b150d216308766e8ea0186fbcef78feff716364008717f4e5d7fc8e2a319d8bb3c753b67bfe2ba66cc34cec5d6b88bbec72b1615cfca02d15554d44016fd8bab4d9dbeca448a5f111dc8746c697dd89b893ca4693a8b8c20f817b459240167671a8b180cb82bc78852adf1a7c1f7f980ab6c43b2d46edd691a95af42233ec21e10f844fab577078653d710fe4d76c4952baf578ac1951a3e20111c4d03d3b56de61b8a31a14e0d0b3de110421955593b4e4aa78c6bdb33a629b33355de0f83fda9802f87c95610a0ae1e9093fd01a6dae327b8f30716495d52b8c62dc731aeb35aa7e257d83754a8c226a26413dc31853cd192a3da9ff61e231a827515467b8d7a8efc9cdb875a530e4ef3ef9ec49c30c1ecd89a33bf57a8e3c196d5e8e010657b589faece9baf1213dec633ae8c779667d77f371b6e615762bcd938c14bbfe6cf6c5e59173900f4134316aafece7eb27bcde9a391ec0a9acdd4ef39a9cd02eb0f654b76b354aa1289273f9c6d372d05bb504dd670f1b5285ae150b72cc5817900a9f7eeecbd373b7c939e125640cd7d2d6b2674bdee3d8d37210e6ec3c3d40d3ca9600cee84ce0362bfe75710aff4957a56f8265e1ecb55994b958544d7699bae7734dc2b481a292b0669b36acd5d7cea8061bf3184dc5f9163c79272efdf1421327df45780be8936dee9036201643d9776888313ad3bab33308c1b6cded33d01147b244e51a6806807fc29f48e204ad50f91f17a3b24f7f738cc4ee7715de53d0efba52030979aaaa1e1d182c5835dd11a0e975f0fe72354bf88669307b4c598b00bf361a9334de28fb22a5024e72c33d05b1aad06d66eeb2ce90aa8947802fed184552e197bf5295ac7d65291aff38c37ad0b00131b8f3556477ee5958fdffb7219797f174ba98f04e444c532725f8c20f8fa15f817b764aa22037f9fdab0665a33c2f074ac67b7c74e5a513c65c682302d08b01c53d0f46ce11dd5acee7f52c8892d612fdbc7eb31b686b42837658db888ab013497982e09805b1d9ecc45457014db1d9e575d6b178cc1b0b7ea0a51b6231ec90b2c468436b4727d263ec5d01750b0803aaf9d9e1a0b6b638a1a603e67552b48ba27a6ba52d05b45f0d81ba1712721e4ce5ebf490158f2a458d04c4351aae2693c4eed392431f3003950be208cbe1b708a7e95979d0be82d77deaa34339344630db9a90cc2d91cd122b83249478acb51ce7fd4a66ca1a748c696e93e3a551fd4248d01fbdd9dd9d47d1f3569e39c9da03635003117eb0a7dcb94f7796162cd6a7bc4e52a1bd917e4c3af2bd8ea6c3d0b12e06b5d56993a299114cc40bb05b4c407ae7bc162b84e8cace4a18157ddcbe5bde566a90834e5725a291a9872a7d799b8f5c3a519582541f55a0cd8cf0c831125032e372610c625a19f454ccd77e91601dff76813e2d39d2203c32d7c7a4d347805d53feb2228f871f06a13043547e55c0e7e6c290cb33b09533fccb67e30594cc36a3d938a7f1b01909bc852e5dc14785f26326904862d2870a4ce808159774fe61c538bf8238a8633e1bf71ae467c2c158b0a3279dac67c3075eb0d520a92ba25e5458d9a54dcf3dfc64a184a7f61cb676dac41b14396a012c7088607ea6046bba0c4dbcac493ff510667d4f67c8b9d1327488d0216eea88bbe4b24f1315c797d06b40b3ed86fd9f850cfb64176a59da51ace79e03c97c684b28b8ecd7219f15da3ad6dfd87dc1a68b94f018cfec9f198cecbee92d1ed883f5b678406475829b2300d920df19de93d9d219b48d7e44ec4324532ba4db1553f592ae6fa5a189d72c4c6f276db812000000057e8b0ca382249daa1ef3fd8c28c79cfa871512b08743634c1062f0c275f9fa8853505909342a94de01997b5363ad61003b734dceee25bc6ccf7db3b8d812c959aa16658bd420ad5670c2e6d4f48d20cb8b59c1a5394c43bd9e8ad5d4fe73696ee9ae6cad9c90163157f755560bddabbca674eccdccc615541ba1370e9f49836fdd5b6b837034aa14181a71944378a9dd0981147c62772af50eeaa8dba0395cb9000000050000000328c6de79547013979bdde5a9b84238ddd0cc7ba91e46eb69f825a676a48496ef4a175cd7a5baa809d21af23a64fe91d40000000b0000000348441a0940099b53c8a003067a64ad6936770fb428c3b99b27a534ecf9b02fdf89bb9df25feb470bf976c2b76d8465efc314edddb097a6a4dc59be096e904a5041f3ba06c424757f9424feb984ea76f0bbc0a06eaa869594502444981b32b659993ffb36cda80f81837151000296eba175ed3eba6f31f60bb8885338aaa569d2dd4ea7c4e9f82432e5035309df15ce2adc98efbbf95447d66df1d1023514145e83d7490bc84e042d71a4d6fddea2f8670d2e81c5e11276844ea2578f0df852a0c05a3de4afaf5dcabc96e20cceb4ab3dc107b5d6cfc63f006e7375efb6efb62cd402fae3ed42f02cc24f10599a394b39f09dede72e6a9d2245dfe74ce9e0df400d7b3abf2a8800a6919d05522a425e91f7d6c309e483faf55b8bf539a341003d5d7c168590748983aeeeb31f1484d0a6c10f3a18b510a29b26f720f11591291c867d9daa7d12e060c00bb208a31d72348994f65f1d8f836a22af4f3d47f288e250ac6320e53bb8a7fb23709d320f5e6d155608da139dfef4f9ff45082f234e33fc884b2efd99ea5308b9ebc18a0196424d9b414b384e941231b0eaff8a82d44d38c0ae812aa1c51b2a4eb64cfb1d0bffed113ab40cbd0eea5c389f85c0d51dc6d215baa84b3677e4a6ccf95944d138eef031a251c4a425613eb79da4639eca5af5094f5e908c96a5035384126aadabc1e6bf0fc6807c6097834039759898c7c4343e807c8662b00ef9e595eac6760a23be594bf6cbfeb59e352300d7ae92d69fff7c5640104353f485388da49a8ce8a2a951cd078f62db1c89c46633355682ed6ee9ddf89eb77bec8a55812ca426096d3fa117a01d9a3887e85c0f59370af11e4aaa80cc7d2d9f5db64c99f24beee0645cea3a116606ec1cd37baaf4ff67913ece3fbc9bd3681aafe5933181bbad99cbcbb8763227cab4343ade4e266dd689e00ddec99613935798f2dba54b5231d493cfcb4f1df888c0f97895f9d0ee7b26c9d75519326c157904d4da30043618a8abf63e4b1f6fbf4ffb1af188f8b7c3c6daa3b32b13e458fedbccd3d7dbf9bee79745b4734f14b50fa622d82ae4ea0f7d20a45498807636210e634767e845f9d0db4e044d4fba294f074ccc52d4f94a84f428e97aa31a7a6be2de7c51f9678b0d6dd2612c91e1029ec852062fa8b3a5b52eb25a7fad9f04d11b81a86675ed540875342315dd04ab1bf8ae84a8a2b6530e601fe3812ff635cb5bb8850efa0b2961bb5bb6afcb249380429bab7d4d26050ce8fe0a433dec32b722e065ff8f26fc41abae5bc98b38440f6a254f2d202e8846ec2aebc318d248dbf22f188ead7f8d5c3294d63e16e681b0045f0ec909a1d260905cac094f3b18b752148ea9747db566c8dc8f61265693a83d4fd334bea679e900efae2937e0b5d20ae78519d8d56948f55ac618db71aba29d9b53edb3fbf99292de3b66364ebe31d7d62d67e5eaad8c5865d090b2d8b2ce11e866438254532ffdcf818630b8bf9037d00b83577e07d15c784b129e1e32cadb0847e3dca0048c9b676efc07f932d2073ca819edd5c3ffb8ad0fe05d79c41ea9843f90eb4b8f0ade112d27c69a9c244726b2ad6b19d91ac66c10d10143c1a05b2273397a848932322cbad23ae5241113b352acdc4d51029c2a06c1a3a1adb448b299b5705971478d05bf3ef0a58639c7d2d1fdbde87de1eb0cc6f569fb437f632ce35210c07402ca8400cbbb6d25b80191e0b465b2dabc4dc4421c95c3a28a0d54d4f41f4eacc33cd417e9fe19ed36f57fd20fe44f90060cc8691e3c8fc879986e0473427f33b449c4a2af0132f8676bb2d3c8acea8b13303f8160a5a5affae34a0429ff54c5e7885b08354530916544e0e5dfb6f998a1b98daea064921b966cad325e12720b538678da2521f05fede749c4c0b99b77225cde8086383ec9b9ed399e4a1809d9cd195323e157a3cb687c77f9dbb36d761e2d11b6646eb3033be553ffc61b402f6008a5dabf1cd20cd51c1f6f1ed5e97382e58f493ff159a662942338298d1e3e5681957aa7cb1c9dee39d44ef88bbb2ea1237ecce134dd87afb42b837b5e2afcde7c30f3be8fc474900ccbb0efcaab5ab6f6c8f26ca3acc19ddd1d38f3ef4750f11c777745d5759c34094cddbe75afb309880014c8151e67a2effe592833d6b56c9de5ba8ed672da11b274b432ab4b769414889b31a9d8b97375ab9defa64bca38be510b668e35a5634cef232bdcf075e7e4f6a2ee6592cbbdc661ef4159ac53b3578559e6db9c0e0e17d14feb4863aa3ea72a9da32eec5dd7c698746f4dbd5079b5cee8375fb733bc0f69d95cb73f8e8e64ed7334ad1debc14501706c76dd8ff69203159ac72218bb09ce97754d0901f32e8298680748341a0db669fd09f6a1b07e06266b756bf801e3653e0daa3a55b8b1341a8015ae89711fbf9a0a183f82457f9f5f5a238cc4b55f7d00069b937ac955fdd35d2b421cf1399f97068be01a1d293e48c33acc8239400142ffe159aec713b6e66634f5b5c874c6e045b0724b0aecf007ab6915b7354ca5af3de12cef2eb417e33d4d0f151eebcff0f48307704840e3deccd62b8bf350366761bd5270aca1ab006ac1c0f6229c166021e13acc429272dadd39c102cbfecd4f12a40735a5b84933630d95d079604285c8cf73eed9898c904ab4d8202f4fe96fde74601f774a8b068ee0645a3dba50d8deb3ccd5d2f5523dd266980f8a6b3f07fe4b829535431bcef5142596cfe75117fd429a39f7d1c13420e7c2eadef795acd6e183e7c15280e390581b933f7bd393a35dee7e0d89c5f32ae3e986ec8ccb6e225aaf7bd8a496a8d1c72a6bd2b2d5286add13633d62022d4109617466dcbb0055c729322d2ecdc2a8efc10b92e61c0e3329ba8624a21cc3380592c0ce055d97e25870a0e7115b0c1c45b6900bb598c8dffe5c7be7fa84e6c0452462539d3e6f6ad51696a34af27f173ba59614a9c36f4e0ed747fb828af5c96c479bf8069bd064c6b718f2c98e61359f18e2137f51fa75b69035aa2e91cc06350b14506c18bf96af898aaf81895bf9d73748eb09b6ba548c4f426d47000000054ad227e9a1ad366cd69116b0b1566201f2a9ebe351c552559a7984ccc4bf3ef045ebb2df1e83f97749337d0b125379d8211afc988301061222a5cd2df951e50bca8496fb7e2d8b5c93a65033e8eae7df3077e95020c1a02c156bc63172125b33f090efbaec44e0dd041c038f0434b60596e857521c652451aa0f3553ce3a4f22cb9376cef3a8836ba932e93cef7b54ffe4a13a6cb2a9fdc9f1383b2c0543670e")]
        public async Task ShouldMonteCarloTestLmsForSampleSuppliedCase(LmsType[] lmsTypes, LmotsType[] lmotsTypes, string message, string seed, string rootI, string expectedSig)
        {
            var subject = new LmsMct(new HssFactory());
            var messageBitString = new BitString(message);
            var seedBitString = new BitString(seed);
            var rootIBitString = new BitString(rootI);
            var expectedSigBitString = new BitString(expectedSig);

            var result = await subject.MCTHashAsync(lmsTypes, lmotsTypes, seedBitString, rootIBitString, messageBitString, true);

            Assert.IsNotNull(result, "null check");
            Assert.IsTrue(result.Success, result.ErrorMessage);

            var resultSig = result.Response[result.Response.Count - 1].Signature;
            System.Console.WriteLine(resultSig.ToHex());
            Assert.AreEqual(expectedSigBitString.BitLength, resultSig.BitLength);
            Assert.AreEqual(expectedSigBitString.ToHex(), resultSig.ToHex());
        }

        [Test]
        public async Task ShouldMonteCarloTestLmsNotSampleWithoutExceptions()
        {
            var subject = new LmsMct(new HssFactory());
            var messageBitString = new BitString("7878787878787878787878787878787878787878787878787878787878787878");
            var seedBitString = new BitString("1212121212121212121212121212121212121212121212121212121212121212");
            var rootIBitString = new BitString("adadadadadadadadadadadadadadadad");

            var mctTask = subject.MCTHashAsync(new LmsType[] { LmsType.LMS_SHA256_M32_H5, LmsType.LMS_SHA256_M32_H5, LmsType.LMS_SHA256_M32_H5 }, new LmotsType[] { LmotsType.LMOTS_SHA256_N32_W4, LmotsType.LMOTS_SHA256_N32_W4, LmotsType.LMOTS_SHA256_N32_W4 }, seedBitString, rootIBitString, messageBitString, false);

            // wait a minute
            await Task.Delay(60000);
            Assert.That(!mctTask.IsFaulted);
            if (mctTask.IsCompleted)
            {
                Assert.That(mctTask.Result.Success);
            }
        }
    }
}