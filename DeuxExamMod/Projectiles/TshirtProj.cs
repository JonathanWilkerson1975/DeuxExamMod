using Terraria;
using System;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;


namespace DeuxExamMod.Projectiles
{
    public class TshirtProj : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.arrow = true;
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            AIType = ProjectileID.WoodenArrowFriendly;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(70, 180, true);

        }

        public override void AI()
        {
            int dust2 = Dust.NewDust(Projectile.Center, 1, 1, 21, 0f, 0f, 0, default(Color), 1f);
            Main.dust[dust2].velocity *= 3f;
            Main.dust[dust2].scale = (float)Main.rand.Next(80, 115) * 0.013f;
            Main.dust[dust2].noGravity = true;
        }
    }
} 