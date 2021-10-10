﻿using System;
using System.Linq;
using GJP2021.Sources.GameStates;
using GJP2021.Sources.Paint;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GJP2021.Sources.Characters
{
    public class Projectile
    {
        private Vector2 _position;
        private readonly float _speed;
        private readonly float _angle;
        private readonly PaintPeriodicSpawner _periodicPaintSpawner;
        private readonly float _duration;
        private float _currentDuration;


        public Projectile(Color color, Vector2 position, float speed, float angle, float duration)
        {
            _position = position;
            _speed = speed;
            _angle = angle;
            _duration = duration;
            _currentDuration = 0F;
            _periodicPaintSpawner =
                new PaintPeriodicSpawner(color, new Color(32, 32, 32), 35, 10, 30, 0.05F, 0.1F, 30);
        }

        public void Update(GameTime gameTime, IngameState gameState, PaintCircles paintCircles)
        {
            _currentDuration += (float)gameTime.ElapsedGameTime.TotalSeconds;
            _periodicPaintSpawner.Update(gameTime, paintCircles, _position);
            var delta = (float) gameTime.ElapsedGameTime.TotalSeconds;
            foreach (var enemy in gameState.Enemies.Where(e => Vector2.Distance(_position, e.Position) < 35))
            {
                enemy.Kill();
            }

            _position.X += _speed * (float)Math.Cos(_angle) * delta;
            _position.Y += _speed * (float)Math.Sin(_angle) * delta;
            
            foreach (var enemy in gameState.Enemies.Where(e => Vector2.Distance(_position, e.Position) <= 35))
            {
                enemy.Kill();
            }
        }

        public bool IsDone()
        {
            return _currentDuration >= _duration;
        }
    }
}