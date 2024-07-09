using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DeuxExamMod.Boss; // Ensure this namespace is correct

namespace DeuxExamMod.Items.Misc // Updated namespace to reflect correct folder
{
    public class StudyGuide : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Study Guide");
            Tooltip.SetDefault("Summons XamLord");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 20;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
            Item.rare = ItemRarityID.Blue;
        }

        public override bool CanUseItem(Player player)
        {
            // Only allow use if no XamLordHead is already alive
            return !NPC.AnyNPCs(ModContent.NPCType<XamLordHead>());
        }

        public override bool? UseItem(Player player)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<XamLordHead>());
            }
            else
            {
                NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, ModContent.NPCType<XamLordHead>());
            }
            return true;
        }
    }
}

