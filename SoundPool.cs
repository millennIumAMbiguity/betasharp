using java.lang;
using java.net;
using java.util;

namespace betareborn
{
    public class SoundPool : java.lang.Object
    {
        private readonly java.util.Random rand = new();
        private readonly Map nameToSoundPoolEntriesMapping = new HashMap();
        private readonly List allSoundPoolEntries = new ArrayList();
        public int numberOfSoundPoolEntries = 0;
        public bool field_1657_b = true;

        public SoundPoolEntry addSound(string var1, java.io.File var2)
        {
            try
            {
                string var3 = var1;
                var1 = var1[..var1.IndexOf('.')];
                if (field_1657_b)
                {
                    while (Character.isDigit(var1[var1.Length - 1]))
                    {
                        var1 = var1[..^1];
                    }
                }

                var1 = var1.Replace('/', '.');
                if (!nameToSoundPoolEntriesMapping.containsKey(var1))
                {
                    nameToSoundPoolEntriesMapping.put(var1, new ArrayList());
                }

                SoundPoolEntry var4 = new(var3, var2.toURI().toURL());
                ((List)nameToSoundPoolEntriesMapping.get(var1)).add(var4);
                allSoundPoolEntries.add(var4);
                ++numberOfSoundPoolEntries;
                return var4;
            }
            catch (MalformedURLException var5)
            {
                var5.printStackTrace();
                throw new RuntimeException(var5);
            }
        }

        public SoundPoolEntry getRandomSoundFromSoundPool(string var1)
        {
            List var2 = (List)nameToSoundPoolEntriesMapping.get(var1);
            return var2 == null ? null : (SoundPoolEntry)var2.get(rand.nextInt(var2.size()));
        }

        public SoundPoolEntry getRandomSound()
        {
            return allSoundPoolEntries.size() == 0 ? null : (SoundPoolEntry)allSoundPoolEntries.get(rand.nextInt(allSoundPoolEntries.size()));
        }
    }

}