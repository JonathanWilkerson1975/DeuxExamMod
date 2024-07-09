using Terraria;
using System;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;


namespace DeuxExamMod.Projectiles
{
	public class BookProj : ModProjectile
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

        public override void AI()
        {
            int dust = Dust.NewDust(Projectile.Center, 1, 1, 15, 0f, 0f, 0, default(Color), 1f);
            Main.dust[dust].velocity *= 0.3f;
            Main.dust[dust].scale = (float)Main.rand.Next(100, 135) * 0.013f;
            Main.dust[dust].noGravity = true;

            int dust2 = Dust.NewDust(Projectile.Center, 1, 1, 137, 0f, 0f, 0, default(Color), 1f);
            Main.dust[dust2].velocity *= 0.3f;
            Main.dust[dust2].scale = (float)Main.rand.Next(80, 115) * 0.013f;
            Main.dust[dust2].noGravity = true;
        }
    }
}