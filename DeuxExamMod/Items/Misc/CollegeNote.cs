using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DeuxExamMod.Items.Misc
{
    public class CollegeNote : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("College Note");
            Tooltip.SetDefault("A note from your college days.");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 99;
            Item.value = 100;
            Item.rare = ItemRarityID.Blue; // Ensure this line is correct
        }
    }
}