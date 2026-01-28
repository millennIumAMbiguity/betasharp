using betareborn.Entities;

namespace betareborn.Biomes
{
    public class BiomeGenHell : BiomeGenBase
    {

        public BiomeGenHell()
        {
            spawnableMonsterList.clear();
            spawnableCreatureList.clear();
            spawnableWaterCreatureList.clear();
            spawnableMonsterList.add(new SpawnListEntry(EntityGhast.Class, 10));

            spawnableMonsterList.add(new SpawnListEntry(EntityPigZombie.Class, 10));
        }
    }

}