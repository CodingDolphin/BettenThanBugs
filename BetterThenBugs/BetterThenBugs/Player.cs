using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BetterThenBugs
{
    
    class Player
    {
        //Spielerwerte
        public float JumpStrength = 450f;
        public bool isGrounded = false;
        public Vector2 Velocity;
        public float PlayerSpeed = 500;
        public float PlayerMaxSpeed = 500;
    
        //Spielerdaten
        public Vector2 PlayerPosition;

        //Animations Variablen
        public  Animation PlayerAnimation;
        public Animation  PlayerSpringAnimation;
        public Animation CurrentPlayerAnimation;
        
        //Lauf Richtung
        public enum walkDirection {Jumping,WalkLeft,WalkRight}
        public walkDirection currentDirection = walkDirection.WalkRight;
        public walkDirection lastDirection = walkDirection.WalkRight;

        public Player()
        {
            PlayerAnimation = new Animation();
            PlayerSpringAnimation = new Animation();
            CurrentPlayerAnimation = PlayerAnimation;
        }


        public void PlayerState()
        {

        }

        //Bewegen

        public void Bewegen(GameTime gameTime, float gravity, float deltaTime, float slowdown)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds / 5;

            if (isGrounded == false)
            {
                Velocity.Y -= gravity * deltaTime;
                PlayerPosition.Y -= Velocity.Y * deltaTime;
            }

            if (PlayerPosition.Y >= 325)
            {
                isGrounded = true;
                Velocity.Y = 0;
                CurrentPlayerAnimation = PlayerAnimation;
            }

            if (isGrounded)
            Velocity.X *= slowdown * deltaTime;

            PlayerPosition.X -= Velocity.X * deltaTime;



            //Bewegungsrichtung

            if (currentDirection == walkDirection.WalkLeft)
            {
                lastDirection = walkDirection.WalkLeft;
                CurrentPlayerAnimation.Direction = SpriteEffects.None;
            }

            if (currentDirection == walkDirection.WalkRight)
            {
                lastDirection = walkDirection.WalkRight;
                CurrentPlayerAnimation.Direction = SpriteEffects.FlipHorizontally;
            }

            if (isGrounded == false & currentDirection == walkDirection.Jumping)
            {
                CurrentPlayerAnimation = PlayerSpringAnimation;
                if(lastDirection == walkDirection.WalkLeft)
                    CurrentPlayerAnimation.Direction = SpriteEffects.None;
                else
                    CurrentPlayerAnimation.Direction = SpriteEffects.FlipHorizontally;

            }

            if (isGrounded == true & Velocity.X >= 10 | Velocity.X <= -10)
                CurrentPlayerAnimation.AnimationActive = true;
            else
            {
                CurrentPlayerAnimation.AnimationActive = false;
                PlayerAnimation.CurrentFrame = 1;
            }
            

        }

        

        }


 }


