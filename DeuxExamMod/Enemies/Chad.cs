using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using DeuxExamMod.Items.Consumeable;  // Ensure the namespace matches where FeelGoodJuice is defined
using DeuxExamMod.Items.Misc;  // Ensure the namespace matches where CollegeNote is defined

namespace DeuxExamMod.Enemies
{
    public class Chad : ModNPC
    {
        private int jumpCooldown = 0; // Cooldown timer for jumping
        private const float FollowDistance = 200f; // Desired distance to maintain from the player
        private int laserCooldown = 0; // Cooldown timer for laser shooting
        private const int LaserCooldownTime = 180; // 3 seconds cooldown (60 frames per second * 3 seconds)

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chad");
            Main.npcFrameCount[NPC.type] = 1; // Set to 1 if the image is a single frame
        }

        public override void SetDefaults()
        {
            NPC.width = 34;
            NPC.height = 40;
            NPC.damage = 30;
            NPC.defense = 15;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit2;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.value = 100f;
            NPC.knockBackResist = 0.4f;
            NPC.aiStyle = 3; // Fighter AI style
            AIType = NPCID.GoblinPeon;
            NPC.noGravity = false; // Ensure gravity affects the NPC
            NPC.noTileCollide = false; // Ensure the NPC collides with tiles
        }

        public override void AI()
        {
            // Custom AI code for Chad with goblin behaviors
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];

            if (player == null || !player.active || player.dead)
            {
                NPC.velocity = Vector2.Zero;
                return;
            }

            Vector2 direction = player.Center - NPC.Center;
            float distance = direction.Length();
            direction.Normalize();

            if (distance > FollowDistance) // Follow player if beyond follow distance
            {
                NPC.velocity = new Vector2(direction.X * 5f, NPC.velocity.Y); // Maintain horizontal movement only
            }
            else // Slow down if within follow distance
            {
                NPC.velocity.X *= 0.9f; // Slow down horizontal movement to maintain distance
            }

            // Add custom jumping logic with cooldown
            if (jumpCooldown <= 0 && Main.rand.NextBool(200)) // Randomly perform a jump attack
            {
                if (NPC.velocity.Y == 0) // Only jump if on the ground
                {
                    NPC.velocity.Y = -10f; // Simulate jump
                    jumpCooldown = 100; // Reset cooldown (approximately 1.6 seconds)
                }
            }
            jumpCooldown--; // Decrease cooldown

            // Laser shooting logic with cooldown
            if (laserCooldown <= 0)
            {
                Vector2 laserDirection = direction;
                laserDirection.Normalize();
                Vector2 laserStartPosition = NPC.Center + new Vector2(0, -10); // Adjust this vector to set laser shooting point

                // Burst of three shots
                for (int i = 0; i < 3; i++)
                {
                    int laserProjectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), laserStartPosition, laserDirection * 10f, ProjectileID.LaserMachinegunLaser, 20, 1f, Main.myPlayer);
                    Main.projectile[laserProjectile].hostile = true;
                    Main.projectile[laserProjectile].friendly = false;
                }

                laserCooldown = LaserCooldownTime; // Reset laser cooldown
            }
            laserCooldown--; // Decrease cooldown
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.dayTime && spawnInfo.Player.ZoneOverworldHeight && NPC.downedBoss3) // Only spawn during the day after Skeletron is defeated
            {
                return 0.2f; // Adjusted spawn chance for goblin behavior
            }
            return 0f;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)
            {
                for (int i = 0; i < 20; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood);
                }
            }
        }

        public override void OnKill()
        {
            int itemType = ModContent.ItemType<CollegeNote>(); // Ensure you have the right namespace and class name here
            if (Main.rand.NextFloat() < 0.30f) // 30% chance to drop
            {
                Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), itemType);
            }
            // Drop FeelGoodJuice (uncommon)
            itemType = ModContent.ItemType<FeelGoodJuice>(); // Ensure you have the right namespace and class name here
            if (Main.rand.NextFloat() < 0.1f) // 10% chance to drop
            {
                Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), itemType);
            }
        }
    }
}