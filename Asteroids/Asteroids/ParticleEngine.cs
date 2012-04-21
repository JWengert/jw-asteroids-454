using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids
{
    public class ParticleEngine
    {
        private Random random;
        public Vector2 EmitterLocation { get; set; }
        private List<Particle> particles;
        private List<Texture2D> textures;
        static public Color[] engineeffects;

        public ParticleEngine(Texture2D t1, Texture2D t2, Vector2 location)
        {
            this.textures = new List<Texture2D>();
            this.textures.Add(t1);
            this.textures.Add(t2);
            EmitterLocation = location;
            this.particles = new List<Particle>();
            random = new Random();
        }

        public void Update(bool ismoving)
        {
            int total = 100;
            if (ismoving)
            {
                for (int i = 0; i < total; i++)
                {
                    particles.Add(GenerateNewParticle());
                }
            }
            for (int particle = 0; particle < particles.Count; particle++)
            {
                particles[particle].Update();
                if (particles[particle].TTL <= 0)
                {
                    particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        private Particle GenerateNewParticle()
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            Vector2 position = EmitterLocation;
            Vector2 velocity = new Vector2(
                                    1f * (float)(random.NextDouble() * 2 - 1),
                                    1f * (float)(random.NextDouble() * 2 - 1));
            float angle = 0;
            float angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
            Color color = new Color(255, 0, 0, 255);
            float size = (float)random.NextDouble() / 2;
            int ttl = 5 + random.Next(40);
            
            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(spriteBatch);
            }
        }

        public void Explosion(int count, Vector2 position)
        {
            Particle newpart;
            for (int i = 0; i < count; i++)
            {
                newpart = GenerateNewParticle();
                newpart.Velocity *= 10;
                newpart.Size *= 2;
                newpart.Position = position;
                if (newpart.Velocity.LengthSquared() > 10)
                {
                    newpart.Velocity.Normalize();
                    newpart.Velocity = newpart.Velocity * random.Next(10);
                }
                particles.Add(newpart);
            }
        }
    }
}
