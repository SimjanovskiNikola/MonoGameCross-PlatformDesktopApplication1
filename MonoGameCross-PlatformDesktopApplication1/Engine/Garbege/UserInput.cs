// using System;
// using System.Collections.Generic;
// using System.Data;
// using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.Graphics;
// using Microsoft.Xna.Framework.Input;
//
// namespace MonoGameCross_PlatformDesktopApplication1; 
//
// public class UserInput : Sprite{
//     
//     public int card_num = -1;
//     public Vector2[] card_position = new Vector2[5];
//     public bool[] pressed = new bool[5];
//
//     public UserInput(Texture2D texture2D, Rectangle rectangle) : base(texture2D, rectangle) {}
//     public UserInput(Dictionary<string, Animation> animations) : base(animations) {}
//
//     public override void Update(GameTime gameTime) {
//         MovementControls();
//         MouseControls();
//     }
//
//     private void MouseControls() { }
//
//     private void MovementControls() {
//         
//         
//
//         if (Keyboard.GetState().IsKeyDown(Keys.D1) && !pressed[0]) {
//             Check_Card(0);
//             pressed[0] = true;
//         }
//         if (Keyboard.GetState().IsKeyDown(Keys.D2) && !pressed[1]) {
//             Check_Card(1);
//             pressed[1] = true;
//         }
//         if (Keyboard.GetState().IsKeyDown(Keys.D3) && !pressed[2]) {
//             Check_Card(2);
//             pressed[2] = true;
//         }
//         if (Keyboard.GetState().IsKeyDown(Keys.D4) && !pressed[3]) {
//             Check_Card(3);
//             pressed[3] = true;
//         }
//         if (Keyboard.GetState().IsKeyDown(Keys.D5) && !pressed[4]) {
//             Check_Card(4);
//             pressed[4] = true;
//         }
//         
//         
//         if (Keyboard.GetState().IsKeyUp(Keys.D1)) {
//             pressed[0] = false;
//         }
//         if (Keyboard.GetState().IsKeyUp(Keys.D2)) {
//             pressed[1] = false;
//         }
//         if (Keyboard.GetState().IsKeyUp(Keys.D3)) {
//             pressed[2] = false;
//         }
//         if (Keyboard.GetState().IsKeyUp(Keys.D4)) {
//             pressed[3] = false;
//         }
//         if (Keyboard.GetState().IsKeyUp(Keys.D5)) {
//             pressed[4] = false;
//         }
//
//
//     }
//
//     public void Check_Card(int num) {
//         if (card_num != -1) {
//             card_position[card_num].Y += 90;
//         }
//         
//         
//         if (card_num == num) {
//             card_num = -1;
//         }
//         else {
//             card_num = num;
//             card_position[card_num].Y -= 90;
//         }
//     }
//     
//     
// }