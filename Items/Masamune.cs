using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static Terraria.ModLoader.PlayerHeadDrawInfo;
namespace BoulderMod.Items
{
	public class Masamune : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Masamune");
			Tooltip.SetDefault("'Try not to burn yourself'");
		}

		public override void SetDefaults() 
		{
			item.damage = 60;
			item.melee = true;
			item.width = 58;
			item.height = 58;
			item.useTime = 16;
			item.useAnimation = 16;
			item.useStyle = 1;
			item.knockBack = 5f;
			item.value = 40000;
			item.rare = 6;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.scale = 1.1f;
        }

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(2701, 100);
            recipe.AddIngredient(ItemID.Muramasa, 1);
            recipe.AddTile(TileID.CrystalBall);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            // Add the Onfire buff to the NPC for 1 second when the weapon hits an NPC
            // 60 frames = 1 second
            target.AddBuff(189, 15);
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(1))
            {
                //Emit dusts when the sword is swung
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 6);

                // Cursed: 74
                // Shadow: 27
                // Frozen: 137
            }
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Items/MasamuneGlow");
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    item.position.X - Main.screenPosition.X + item.width * 0.5f,
                    item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }
    }
}