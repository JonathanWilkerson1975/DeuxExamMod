using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DeuxExamMod.Items.Weapon.Melee
{
	public class FratPad : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frat Pad");
            Tooltip.SetDefault("Phi Theta Kappa! The power of fraternities flows through the paddle.");
        }


        public override void SetDefaults()
		{
			Item.damage = 60;
			Item.DamageType = DamageClass.Melee;
			Item.crit = 16;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 30;
			Item.useAnimation = 20;
			Item.useStyle = 1;
			Item.knockBack = 8;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}
	}
}