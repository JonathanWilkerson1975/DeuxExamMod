using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace DeuxExamMod.Items.Weapon.Melee
{
    public class BookMark : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Book Mark");
            Tooltip.SetDefault("Send the razor sharp edges into their eyes.");
        }

        public override void SetDefaults() // Meant to be stupid broken and slow attacking
        {
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.damage = 600;
            Item.crit = 1;
            Item.width = 20;
            Item.height = 20;
            Item.useTime = 50;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 50;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.rare = ItemRarityID.Purple;
            Item.shoot = Mod.Find<ModProjectile>("BookProj").Type;
            Item.shootSpeed = 12f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

    }

}
