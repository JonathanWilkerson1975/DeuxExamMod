using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace DeuxExamMod.Items.Weapon.Range
{
    public class TShirtCannon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("T-Shirt Cannon");
            Tooltip.SetDefault("Everybody get a T-Shirt!!!.");
        }

        public override void SetDefaults()
        {
            Item.damage = 240;
            Item.DamageType = DamageClass.Ranged;
            Item.crit = 6;
            Item.width = 80;
            Item.height = 80;
            Item.useAnimation = 80;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 7.5f;
            Item.rare = ItemRarityID.Blue;
            Item.shoot = Mod.Find<ModProjectile>("TshirtProj").Type;
            Item.UseSound = SoundID.Item61;
            Item.autoReuse = true;
            Item.shootSpeed = 10;
            Item.useTime = 80;

        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1f, 0f);
        }
    }
}