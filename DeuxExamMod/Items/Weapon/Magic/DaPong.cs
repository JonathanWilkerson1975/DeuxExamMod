using DeuxExamMod.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace DeuxExamMod.Items.Weapon.Magic
{
    public class DaPong : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("DaPong");
            Tooltip.SetDefault("Pong time.");
        }

        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Magic;
            Item.damage = 20;
            Item.mana = 2;
            Item.crit = 6;
            Item.width = 20;
            Item.height = 20;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.rare = ItemRarityID.Blue;
            Item.shoot = Mod.Find<ModProjectile>("DaPongProj").Type;
            Item.shootSpeed = 12;
            Item.UseSound = SoundID.Item9;
            Item.autoReuse = true;

        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2f, 1f);
        }
    }
}