using System.Collections.Generic; // Ensure this is included
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework; // Ensure this is included
using DeuxExamMod.Items.Consumeable;
using DeuxExamMod.Items.Misc;
using DeuxExamMod.Items.Accessories;
using DeuxExamMod.Items.Weapon.Melee;  // Namespace for FratPad
using DeuxExamMod.Items.Weapon.Range;  // Namespace for TShirtCannon
using DeuxExamMod.Items.Weapon.Magic;  // Correct namespace for DaPong
using DeuxExamMod.Projectiles; // Ensure this is included
using DeuxExamMod.Buff; // Ensure this is included
using Terraria.GameContent.ItemDropRules; // Add Tmodloader's drop rules

namespace DeuxExamMod.Boss
{
    public class XamLordHead : ModNPC
    {
        private int laserTimer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("XamLord Head");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.SkeletronHead];
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.SkeletronHead);
            NPC.width = 20;
            NPC.height = 20;
            NPC.damage = 15;
            NPC.defense = 8;
            NPC.lifeMax = 100;
            NPC.value = 100f;
            NPC.knockBackResist = 0.4f;
            NPC.boss = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.aiStyle = -1; // Custom AI
            laserTimer = 0; // Initialize the laser timer
        }

        public override void AI()
        {
            // Custom AI code to follow the player constantly
            Player player = Main.player[NPC.target];

            if (!player.active || player.dead)
            {
                NPC.TargetClosest(true);
                player = Main.player[NPC.target];
                if (!player.active || player.dead)
                {
                    NPC.velocity = new Vector2(0f, 10f); // Move downwards and despawn
                    if (NPC.timeLeft > 10)
                    {
                        NPC.timeLeft = 10;
                    }
                    return;
                }
            }

            float speed = 6f;
            Vector2 targetPosition = player.Center;
            Vector2 direction = targetPosition - NPC.Center;
            float distance = direction.Length();
            direction.Normalize();

            if (distance > 300f) // If too far from player, move closer
            {
                NPC.velocity = (NPC.velocity * 20f + direction * speed) / 21f;
            }
            else if (distance > 50f) // Maintain a small distance from player
            {
                NPC.velocity = (NPC.velocity * 20f + direction * (speed * 0.5f)) / 21f;
            }
            else // If too close, adjust speed to avoid collision
            {
                NPC.velocity *= 0.9f;
            }

            // Ensure NPC faces the player
            NPC.spriteDirection = NPC.direction = NPC.Center.X < player.Center.X ? 1 : -1;

            // Laser shooting logic
            laserTimer++;
            if (laserTimer > 60) // Shoot every second (60 ticks)
            {
                laserTimer = 0;
                ShootLaserAtPlayer(player);
            }
        }

        private void ShootLaserAtPlayer(Player player)
        {
            Vector2 direction = player.Center - NPC.Center;
            direction.Normalize();
            direction *= 10f; // Speed of the laser

            int damage = 20; // Set the damage of the laser
            int type = ProjectileID.DeathLaser; // Set the type of the laser (you can change to any projectile ID you want)

            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, direction, type, damage, 1f, Main.myPlayer);
        }


        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {

            npcLoot.Add(ItemDropRule.OneFromOptions(4,
                ModContent.ItemType<Pillow>(),
                ModContent.ItemType<SadSoul>(),
                ModContent.ItemType<FeelGoodJuice>()));

            npcLoot.Add(ItemDropRule.OneFromOptions(4,
                ModContent.ItemType<FratPad>(),
                ModContent.ItemType<DaPong>(),
                ModContent.ItemType<TShirtCannon>()));
        }
    }
}
