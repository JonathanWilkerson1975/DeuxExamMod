using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using DeuxExamMod.Items.Misc;  // Ensure the namespace matches where CollegeNote is defined
using DeuxExamMod.Items.Weapon.Melee;  // Ensure the namespace matches where BookMark is defined
using DeuxExamMod.Items.Consumeable;  // Ensure the namespace matches where FeelGoodJuice is defined
using DeuxExamMod.Projectiles;  // Ensure this matches the namespace for EbookusProj

namespace DeuxExamMod.Enemies
{
    public class Ebookus : ModNPC
    {
        private int shootingCooldown = 60; // Cooldown for shooting
        private int changeDirectionCooldown = 20; // Cooldown for changing direction
        private const float FollowDistance = 200f; // Desired distance to maintain from the player

        public int item1Type = ModContent.ItemType<FeelGoodJuice>();
        public int item2Type = ModContent.ItemType<CollegeNote>();
        public int item1Amount = 1;
        public int item2Amount = 1;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ebookus");
            Main.npcFrameCount[NPC.type] = 3; // Sets vertical heigh of the sprite... which kind of makes no sense.
        }

        public override void SetDefaults()
        {
            NPC.width = 36;
            NPC.height = 36;
            NPC.damage = 10;
            NPC.defense = 20;
            NPC.lifeMax = 50;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = 100f;
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
        }

        public override void AI()
        {
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];
            if (!player.dead && player.active)
            {
                FollowPlayer(player);
                ShootAtPlayer(player);
            }
            HandleAnimation();
        }

        private void FollowPlayer(Player player)
        {
            Vector2 moveDirection = player.Center - NPC.Center;
            bool isPlayerOnGround = player.velocity.Y == 0; // Check if the player is on the ground

            if (isPlayerOnGround)
            {
                // Set the NPC to follow 8 tiles above the player
                Vector2 targetPosition = new Vector2(player.Center.X, player.Center.Y - 128); // 128 pixels = 8 tiles
                moveDirection = targetPosition - NPC.Center;
            }
            else
            {
                // Change direction randomly to mimic erratic behavior
                changeDirectionCooldown--;
                if (changeDirectionCooldown <= 0)
                {
                    moveDirection = moveDirection.RotatedByRandom(MathHelper.ToRadians(45)); // Randomize direction within 45 degrees
                    changeDirectionCooldown = Main.rand.Next(15, 30); // Random cooldown between changes
                }
            }

            // Normalize direction vector and apply velocity
            moveDirection.Normalize();
            NPC.velocity = (NPC.velocity * 0.9f) + moveDirection * 0.5f;
        }

        private void ShootAtPlayer(Player player)
        {
            shootingCooldown--;
            if (shootingCooldown <= 0)
            {
                Vector2 shootDirection = player.Center - NPC.Center;
                shootDirection.Normalize();
                shootDirection *= 10f; // Speed of the projectile

                int projectileType = ModContent.ProjectileType<EbookusProj>(); // Correctly reference the projectile type
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, shootDirection, projectileType, NPC.damage / 2, 0, Main.myPlayer);

                shootingCooldown = 60; // Reset cooldown
            }
        }

        private void HandleAnimation()
        {
            NPC.frameCounter++;
            if (NPC.frameCounter > 10f) // Adjust the speed of the animation by changing this value
            {
                NPC.frameCounter = 0;
                NPC.frame.Y = (NPC.frame.Y + NPC.frame.Height) % (Main.npcFrameCount[NPC.type] * NPC.frame.Height);
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            bool isNightTime = !Main.dayTime;
            bool hasDefeatedSkeletron = NPC.downedBoss3;
            bool playerNearSurface = spawnInfo.Player.ZoneOverworldHeight;
            bool notUnderground = spawnInfo.SpawnTileY < Main.worldSurface;

            if (isNightTime && hasDefeatedSkeletron && playerNearSurface && notUnderground)
            {
                return 1f; // 100% chance to spawn at night after Skeletron is defeated and above ground
            }

            return 0f;
        }

        public override void OnKill()
        {
            // Drop CollegeNote (common)
            if (Main.rand.NextFloat() < 0.75f) // 75% chance to drop
            {
                Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), item1Type, item1Amount);
            }

            // Drop Bookmark (uncommon)
            if (Main.rand.NextFloat() < 0.25f) // 25% chance to drop
            {
                Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), ModContent.ItemType<BookMark>());
            }
        }
    }
}