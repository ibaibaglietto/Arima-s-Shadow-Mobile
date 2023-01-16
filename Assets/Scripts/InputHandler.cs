using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    //We create a dictionary to bind commands to keys
    Dictionary<string, Command> keys = new Dictionary<string, Command>();
    //The number of commands that we will save
    private static int max = 1250;
    //An array of command arrays that we will use to save all the commands that the player will do
    private Command[,] previousCommands = new Command[max,10];
    //The number of the command array that we are executing now
    private int lastCommands = 0;
    //Two variables to save the commands that will be saved in the previous commands and sent to the skeletons
    private Command[] commands = new Command[10];
    private Command[] sendCommands = new Command[10];
    //The number of commands saved in this command array, can be a maximum of 10
    private int totalCommands = 0;
    //Arrays of vectors to save the position and velocity of the player, to mitigate the error that can be caused when the followers use the commands that the player generated
    private Vector3[] lastPos = new Vector3[100];
    private Vector3[] lastVel = new Vector3[100];
    //The commands
    private Command moveLeftPress;
    private Command moveLeftRelease;
    private Command moveRightPress;
    private Command moveRightRelease;
    private MoveCommand move;
    private Command jumpPress;
    private Command jumpRelease;
    private Command dashPress;
    private Command dashRelease;
    //The keycodes of the 4 movement commands
    private KeyCode moveLeftKey;
    private KeyCode moveRightKey;
    private KeyCode jumpKey;
    private KeyCode dashKey;
    //Ints to know if a key is being pressed
    private int moveLeft;
    private int moveRight;
    private int jump;
    private int dash;
    //Booleans to know if a button has been pressed
    private bool startDash;
    private bool endDash;
    private bool startJump;
    private bool endJump;
    private bool startLeft;
    private bool endLeft;
    private bool startRight;
    private bool endRight;
    //The player input
    private PlayerInput playerInput;
    //The last speed
    private float lastSpeed = 0.0f;
    //A boolean to know if we have already moved the player this fixedupdate and an int to know where it is stored
    private bool moved = false;
    private int movedPos;

    private void Start()
    {
        //We find the player input
        playerInput = GetComponent<PlayerInput>();
        //We save the strings that will be connected to the commands
        //moveLeftPress = PlayerPrefs.GetString("moveLeft") + "ButtonPress";
        //moveLeftRelease = PlayerPrefs.GetString("moveLeft") + "ButtonRelease";
        //moveRightPress = PlayerPrefs.GetString("moveRight") + "ButtonPress";
        //moveRightRelease = PlayerPrefs.GetString("moveRight") + "ButtonRelease";
        //jumpPress = PlayerPrefs.GetString("jump") + "ButtonPress";
        //jumpRelease = PlayerPrefs.GetString("jump") + "ButtonRelease";
        //dashPress = PlayerPrefs.GetString("dash") + "ButtonPress";
        //dashRelease = PlayerPrefs.GetString("dash") + "ButtonRelease";
        //We add to the dictionary the strings with their connected command.
        //keys.Add(moveLeftPress, new MoveLeftCommand());
        //keys.Add(moveLeftRelease, new StopMoveLeftCommand());
        //keys.Add(moveRightPress, new MoveRightCommand());
        //keys.Add(moveRightRelease, new StopMoveRightCommand());
        //keys.Add(jumpPress, new JumpCommand());
        //keys.Add(jumpRelease, new StopJumpCommand());
        //keys.Add(dashPress, new DashCommand());
        //keys.Add(dashRelease, new StopDashCommand());
        moveLeftPress = new MoveLeftCommand();
        moveLeftRelease = new StopMoveLeftCommand();
        moveRightPress = new MoveRightCommand();
        moveRightRelease = new StopMoveRightCommand();
        jumpPress = new JumpCommand();
        jumpRelease = new StopJumpCommand();
        dashPress = new DashCommand();
        dashRelease = new StopDashCommand();
        //Using refraction, we save the keycodes that will activate the commands
        //moveLeftKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("moveLeft"));
        //moveRightKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("moveRight"));
        //jumpKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jump"));
        //dashKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("dash"));
        startDash = false;
        endDash = false;
        startJump = false;
        endJump = false;
        startLeft = false;
        endLeft = false;
        startRight = false;
        endRight = false;
    }
    //We save a maximum of 10 commands that will be sent every 1/50 seconds
    public void HandleInput()
    {
        //Debug.Log(playerInput.actions["Move"].ReadValue<Vector2>().x);
        if (totalCommands < 10)
        {
            if (startJump)
            {
                commands[totalCommands] = jumpPress;
                totalCommands++;
                jump = 1;
                startJump = false;
            }
            if (endJump)
            {
                commands[totalCommands] = jumpRelease;
                totalCommands++;
                jump = 0;
                endJump = false;
            }
            if( Mathf.Abs(lastSpeed - playerInput.actions["Move"].ReadValue<Vector2>().x) > 0.01f)
            {
                lastSpeed = playerInput.actions["Move"].ReadValue<Vector2>().x;
                if (!moved)
                {
                    commands[totalCommands] = new MoveCommand(lastSpeed);
                    movedPos = totalCommands;
                    moved = true;
                    totalCommands++;
                }
                else
                {
                    commands[movedPos] = new MoveCommand(lastSpeed);
                }
            }
            if (startLeft)
            {
                if (moveLeft == 0)
                {
                    commands[totalCommands] = moveLeftPress;
                    totalCommands++;
                    moveLeft = 1;
                }                    
                startLeft = false;
            }
            if (endLeft)
            {
                if(moveLeft == 1)
                {
                    commands[totalCommands] = moveLeftRelease;
                    totalCommands++;
                    moveLeft = 0;
                }
                endLeft = false;
            }
            if (startRight)
            {
                if(moveRight == 0)
                {
                    commands[totalCommands] = moveRightPress;
                    totalCommands++;
                    moveRight = 1;
                }
                startRight = false;
            }
            if (endRight)
            {
                if(moveRight == 1)
                {
                    commands[totalCommands] = moveRightRelease;
                    totalCommands++;
                    moveRight = 0;
                }
                endRight = false;
            }
            if (startDash)
            {
                commands[totalCommands] = dashPress;
                totalCommands++;
                dash = 1;
                startDash = false;
            }
            if (endDash)
            {
                commands[totalCommands] = dashRelease;
                totalCommands++;
                dash = 0;
                endDash = false;
            }
        }
    }
    public void CheckInput()
    {
        if (totalCommands < 10)
        {
            if (jump == 1)
            {
                commands[totalCommands] = jumpRelease;
                totalCommands++;
                jump = 0;
            }
            if (moveLeft == 1)
            {
                commands[totalCommands] = moveLeftRelease;
                totalCommands++;
                moveLeft = 0;
            }
            if (moveRight == 1)
            {
                commands[totalCommands] = moveRightRelease;
                totalCommands++;
                moveRight = 0;
            }
            if (dash == 1)
            {
                commands[totalCommands] = dashRelease;
                totalCommands++;
                dash = 0;
            }
        }
    }
    //We return the saved commands and save the possition and velocity every 0.5 seconds
    public Command[] ReturnInput(Vector3 pos, Vector3 vel)
    {
        if (lastCommands % 25 == 0)
        {
            lastPos[lastCommands / 25] = pos;
            lastVel[lastCommands / 25] = vel;
        }
        for (int i = 0; i < 10; i++)
        {
            previousCommands[lastCommands, i] = commands[i];
        }
        lastCommands++;
        if (lastCommands >= max)
        {
            lastCommands = 0;
        }
        sendCommands = commands;
        commands = new Command[10]; 
        movedPos = 0;
        moved = false;
        totalCommands = 0;
        return sendCommands;
    }
    //A function to get the previously saved position, velocity and commands
    public Command[,] GetPreviousCommands(out Vector3 position, out Vector3 velocity, int numb)
    {
        position = lastPos[numb];
        velocity = lastVel[numb];
        return previousCommands;
    }
    //
    public void StartDash()
    {
        startDash = true;
    }
    //
    public void EndDash()
    {
        endDash = true;
    }
    //
    public void StartJump()
    {
        startJump = true;
    }
    //
    public void EndJump()
    {
        endJump = true;
    }
    //
    public void StartLeft()
    {
        startLeft = true;
    }
    //
    public void EndLeft()
    {
        endLeft = true;
    }
    //
    public void StartRight()
    {
        startRight = true;
    }
    //
    public void EndRight()
    {
        endRight = true;
    }
}
