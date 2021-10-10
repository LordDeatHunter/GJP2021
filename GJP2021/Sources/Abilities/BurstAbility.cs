﻿using System;
using GJP2021.Sources.Characters;
using GJP2021.Sources.Paint;
using Microsoft.Xna.Framework;

namespace GJP2021.Sources.Abilities
{

    public class BurstAbility : IAbility
    {
        public static readonly BurstAbility Instance = new();
        public override int PaintCost => 20;

        private readonly PaintSpawner _paintSpawner = new(PaintCircle.ColorMap[PaintColors.RED], new Color(64, 64, 64),
            150, 30, 70, 0.5F, 30);

        private readonly Random _random = new();
        private BurstAbility() {}

        protected override void Use(Player player, PaintCircles paintCircles)
        {
            _paintSpawner.SetColor(PaintCircle.ColorMap[player.TrailColor]);
            var max = 16 + _random.Next(17);
            for (var i = 0; i < max; ++i)
            {
                paintCircles.Add(_paintSpawner.SpawnCircle(player.Position));
            }
            player.Damage(PaintCost);
        }

        public override bool CanUse(Player player)
        {
            return player.TrailColor == PaintColors.RED && player.Health >= PaintCost + 5;
        }
        
    }
    
}