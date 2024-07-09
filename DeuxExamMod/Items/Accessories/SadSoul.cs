using DeuxExamMod.Projectiles;
using DeuxExamMod.Buff;
using Terraria;
using System;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace DeuxExamMod.Items.Accessories
{
    public class SadSoul : ModItem
    {
        public new string LocalizationCategory => "Items.Pets";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sad Soul");
            Tooltip.SetDefault("It didn't want to go, but it was taken too soon.");

        }


        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ZephyrFish); // Basic properties of the zephyrfish summon item
            Item.buffType = Mod.Find<ModBuff>("GhostDogBuff").Type; // Applies summon pet buff
            Item.shoot = Mod.Find<ModProjectile>("GhostDogProj").Type; // Summon pet
            Item.rare = ItemRarityID.Purple;
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                player.AddBuff(Item.buffType, 3600);
            }
            return true;
        }

    }
}