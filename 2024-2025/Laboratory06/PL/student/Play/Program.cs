using Common;
using Play;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using System.Text;
using Task;





Map map = new(Levels.level_1_rooms, Levels.level_1_items);
EnemyController.EC = new EnemyController(map);

float angle = 0;
Vector2 position = new(3, 3);

FrameTimer timer = new FrameTimer();
int movementCycles = 1;
while (true)
{
    timer.StartFrame();
    (position, angle) = Controls.ProcessMovement(position, angle, map);
    Caster.Print(position, angle, map);
    if (timer.Time*0.2f > movementCycles)
    {
        EnemyController.EC.Update();
        movementCycles++;
    }
    timer.EndFrame();
}