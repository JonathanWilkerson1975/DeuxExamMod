using Terraria;
using System;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;


namespace DeuxExamMod.Projectiles
{
	public class DaPongProj : ModProjectile
	{

		public override void SetDefaults()
		{
			//Projectile.
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.aiStyle = 24;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
		}

		public override void AI()
		{
			Projectile.aiStyle = 305;
			Lighting.AddLight(Projectile.position, 0.2f, 0.2f, 0.6f);
			Lighting.Brightness(1, 1);
			int dust2 = Dust.NewDust(Projectile.Center, 1, 1, 164, 0f, 0f, 0, default(Color), 0f);
			Main.dust[dust2].velocity *= 8f;
			Main.dust[dust2].scale = (float)Main.rand.Next(80, 115) * 0.003f;
			Main.dust[dust2].noGravity = true;

		}
	}
}