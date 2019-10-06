using System;
using System.Collections.Generic;

namespace MyHero.Controllers
{
    static class ImageController
    {

        static public string GetImage()
        {
            List<String> Images = new List<String> { "AdobeStock_204363582_Preview.jpeg", "AdobeStock_285509639_Preview.jpeg", "AdobeStock_165514310_Preview.jpeg", "AdobeStock_77366366_Preview.jpeg", "AdobeStock_64246853_Preview.jpeg", "AdobeStock_226253055_Preview.jpeg", "AdobeStock_128715849_Preview.jpeg", "AdobeStock_277334796_Preview.jpeg", "AdobeStock_226902148_Preview.jpeg", "AdobeStock_263675781_Preview.jpeg", "AdobeStock_128715886_Preview.jpeg", "AdobeStock_247244451_Preview.jpeg", "AdobeStock_107841841_Preview.jpeg", "AdobeStock_209442433_Preview.jpeg", "AdobeStock_66851636_Preview.jpeg", "AdobeStock_225577237_Preview.jpeg", "AdobeStock_209442402_Preview.jpeg", "AdobeStock_110540015_Preview.jpeg", "AdobeStock_266514331_Preview.jpeg", "AdobeStock_71063477_Preview.jpeg", "AdobeStock_227441237_Preview.jpeg", "AdobeStock_199547256_Preview.jpeg", "AdobeStock_208477258_Preview.jpeg", "AdobeStock_198957603_Preview.jpeg", "AdobeStock_284291842_Preview.jpeg", "AdobeStock_121867143_Preview.jpeg", "AdobeStock_117393895_Preview.jpeg", "AdobeStock_198957905_Preview.jpeg", "AdobeStock_79502693_Preview.jpeg", "AdobeStock_128737210_Preview.jpeg", "AdobeStock_204363511_Preview.jpeg", "AdobeStock_282018086_Preview.jpeg", "AdobeStock_280828562_Preview.jpeg", "AdobeStock_148902626_Preview.jpeg", "AdobeStock_108951765_Preview.jpeg", "AdobeStock_224137584_Preview.jpeg", "AdobeStock_103524426_Preview.jpeg", "AdobeStock_86185490_Preview.jpeg", "AdobeStock_204363557_Preview.jpeg", "AdobeStock_266514408_Preview.jpeg", "AdobeStock_292741393_Preview.jpeg", "AdobeStock_96769148_Preview.jpeg", "AdobeStock_285509497_Preview.jpeg", "AdobeStock_283242505_Preview.jpeg", "AdobeStock_91477359_Preview.jpeg", "AdobeStock_108326652_Preview.jpeg", "AdobeStock_111841639_Preview.jpeg", "AdobeStock_133776052_Preview.jpeg", "AdobeStock_226901856_Preview.jpeg", "AdobeStock_83241176_Preview.jpeg", "AdobeStock_161060256_Preview.jpeg", "AdobeStock_71063402_Preview.jpeg", "AdobeStock_98090660_Preview.jpeg", "AdobeStock_77996857_Preview.jpeg", "AdobeStock_226902191_Preview.jpeg", "AdobeStock_199546477_Preview.jpeg", "AdobeStock_79502753_Preview.jpeg" };
            Random ran = new Random();
            int index = ran.Next(Images.Count);
            return "/stock/" + Images[index];
        }
    }
}
