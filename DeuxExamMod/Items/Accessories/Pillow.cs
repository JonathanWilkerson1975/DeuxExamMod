using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DeuxExamMod.Items.Accessories
{
    public class Pillow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pillow");
            Tooltip.SetDefault("Increase summon damage 8%\nIncrease regen\nStronger regen when standing still\n''You look sleepy, I can fix that... ;)''");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.rare = ItemRarityID.Purple;

        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // Passively give an 8% damage boost
            player.GetDamage(DamageClass.Summon) += 0.08f; // Increases **Should be summon damage since there is no summon weapon added** damage by 8%

            // Check player's movement for HP regen rate
            if (player.velocity.X == 0 && player.velocity.Y == 0)
            {
                // Higher regen rate when standing still
                player.lifeRegen += 10; // HP per minute
            }
            else
            {
                // Lower regen rate while moving
                player.lifeRegen += 5; // HP per minute
            }
        }
    }
}