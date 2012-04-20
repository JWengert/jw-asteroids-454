﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Asteroids
{
    public class Particle
    {

        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public float Angle { get; set; }
        public float AngularVelocity { get; set; }
        public Color color { get; set; }
        public float Size { get; set; }
        public int TTL { get; set; }
        public int maxttl, effect;
        public float r, g, b, a;

        public Particle(Texture2D texture, Vector2 position, Vector2 velocity,
            float angle, float angularVelocity, Color color, float size, int ttl)
        {
            Texture = texture;
            Position = position;
            Velocity = velocity;
            Angle = angle;
            AngularVelocity = angularVelocity;
            //this.color = color;
            r = 255;
            b = 0;
            g = 0;
            a = 255;
            Size = size;
            TTL = ttl;
            maxttl = TTL;
        }

        public void Update()
        {
            TTL--;
            float ratio = (float)(maxttl-TTL)/(float)maxttl;
            Position += Velocity;
            Angle += AngularVelocity;
            //effect = (int) Math.Floor((double)ParticleEngine.engineeffects.Length - ((double)TTL / (double)maxttl) * ((double)(ParticleEngine.engineeffects.Length - 1)));
            r = (1 - ratio);
            g = (0 + ratio * 0.5f);
            b = (0 + ratio);
            a = (1 - ratio);
            color = new Color(r, g, b, a);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);

            spriteBatch.Draw(Texture, Position, sourceRectangle, color, Angle, origin, Size, SpriteEffects.None, 0f);
        }
    }
}
