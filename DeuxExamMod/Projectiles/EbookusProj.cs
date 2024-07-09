using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DeuxExamMod.Projectiles
{
	public class EbookusProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ebookus Projectile");
		}

		public override void SetDefaults()
		{
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.aiStyle = 1;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 300;
			AIType = ProjectileID.Stinger; // This will copy the behavior of the stinger
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(BuffID.Poisoned, 120); // Example debuff
		}
	}
}
