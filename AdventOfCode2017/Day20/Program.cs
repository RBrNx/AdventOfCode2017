using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Day20
{
    class Particle
    {
        public Vector3 Position;
        public Vector3 Velocity;
        public Vector3 Acceleration;
        public int id;

        public Particle(Vector3 pos, Vector3 vel, Vector3 acc, int _id)
        {
            Position = pos;
            Velocity = vel;
            Acceleration = acc;
            id = _id;
        }

        public Particle(Particle p)
        {
            Position = new Vector3(p.Position.x, p.Position.y, p.Position.z);
            Velocity = new Vector3(p.Velocity.x, p.Velocity.y, p.Velocity.z);
            Acceleration = new Vector3(p.Acceleration.x, p.Acceleration.y, p.Acceleration.z);
            id = p.id;
        }

        public override bool Equals(object obj)
        {
            Particle p = (Particle)obj;
            return (id == p.id);
        }

        public override int GetHashCode()
        {
            return id;
        }

        public void IncreaseVelocity(Vector3 acc)
        {
            Velocity.x += acc.x;
            Velocity.y += acc.y;
            Velocity.z += acc.z;
        }

        public void IncreasePosition(Vector3 vel)
        {
            Position.x += vel.x;
            Position.y += vel.y;
            Position.z += vel.z;
        }

        public void Move()
        {
            IncreaseVelocity(Acceleration);
            IncreasePosition(Velocity);
        }

        public bool isEqualPosition(Particle p)
        {
            return (Position.x == p.Position.x && Position.y == p.Position.y && Position.z == p.Position.z);
        }

        public int DistanceFrom(Vector3 point)
        {
            return Math.Abs(Position.x - point.x) + Math.Abs(Position.y - point.y) + Math.Abs(Position.z - point.z);
        }
    }

    class Vector3
    {
        public int x;
        public int y;
        public int z;

        public Vector3(int _x, int _y, int _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }

        public Vector3(int[] arr)
        {
            x = arr[0];
            y = arr[1];
            z = arr[2];
        }
    }

    class Program
    {

        static void PartOne(List<Particle> particles) {
            Dictionary<Particle, int> closestParticle = new Dictionary<Particle, int>();

            for (int i = 0; i < 5000; i++)
            {
                int smallestDist = int.MaxValue;
                Particle closest = null;

                foreach (Particle p in particles)
                {
                    int dist = p.DistanceFrom(new Vector3(0, 0, 0));
                    if (dist < smallestDist)
                    {
                        smallestDist = dist;
                        closest = p;
                    }
                    p.Move();
                }

                if (closestParticle.ContainsKey(closest))
                {
                    closestParticle[closest]++;
                }
                else
                {
                    closestParticle.Add(closest, 1);
                }
            }

            Console.WriteLine("The particle that will stay closest to <0, 0, 0> is particle: " + closestParticle.FirstOrDefault(x => x.Value == closestParticle.Values.Max()).Key.id);
        }

        static void PartTwo(List<Particle> particles) {
            for (int i = 0; i < 5000; i++)
            {
                List<Particle> collided = new List<Particle>();

                foreach (Particle particle in particles)
                {
                    foreach (Particle p in particles)
                    {
                        if (!particle.Equals(p) && particle.isEqualPosition(p))
                        {
                            if(!collided.Contains(particle)) collided.Add(particle);
                        }
                    }
                }

                particles = particles.Except(collided).ToList();

                foreach (Particle p in particles)
                {
                    p.Move();                    
                } 
            }

            Console.WriteLine("There are " + particles.Count() + " particles left");
        }

        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("../../input.txt");
            List<Particle> particles = new List<Particle>();
            List<Particle> collided = new List<Particle>();
            

            for(int i = 0; i < lines.Length; i++)
            {
                MatchCollection matches = Regex.Matches(lines[i], @"(?:\w+)=<(-?\d+,-?\d+,-?\d+)>");
                Vector3 pos = new Vector3(matches[0].Groups[1].Value.Split(',').Select(x => int.Parse(x)).ToArray());
                Vector3 vel = new Vector3(matches[1].Groups[1].Value.Split(',').Select(x => int.Parse(x)).ToArray());
                Vector3 acc = new Vector3(matches[2].Groups[1].Value.Split(',').Select(x => int.Parse(x)).ToArray());
                particles.Add(new Particle(pos, vel, acc, i));
            }

            PartOne(particles.Select(x => new Particle(x)).ToList());
            PartTwo(particles.Select(x => new Particle(x)).ToList());

            Console.Read();
        }
    }
}
