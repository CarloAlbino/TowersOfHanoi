using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowersOfHanoiText : MonoBehaviour {

    private Stack<int> towerA;
    private Stack<int> towerB;
    private Stack<int> towerC;

    private const int NUM_OF_DISK = 5;

	void Start ()
    {
        // Initialize the first tower
        towerA = new Stack<int>();
        for (int i = 0; i < NUM_OF_DISK; i++)
        {
            towerA.Push(i);
        }
        // Initialize empty towers
        towerB = new Stack<int>();
        towerC = new Stack<int>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            GameStep();
        }
	}

    private void GameStep()
    {
        print("A - " +
        StackString(towerA) +
        "B - " +
        StackString(towerB) +
        "C - " +
        StackString(towerC));
        Move(towerA.Count, towerA, towerC, towerB);
    }

    private void Move(int disk, Stack<int> sourceTower, Stack<int> targetTower, Stack<int> auxiliaryTower)
    {
        if(disk > 0)
        {
            // Move disk-1 disks from source to auxiliary, so they are out of the way
            Move(disk - 1, sourceTower, auxiliaryTower, targetTower);
            // Move the nth disk from source to target
            targetTower.Push(sourceTower.Pop());
            // Display progress
            print("A - " +
            StackString(towerA) +
            "B - " +
            StackString(towerB) +
            "C - " +
            StackString(towerC));
            // Move the disk-1 disk that we left on the auxiliary onto target
            Move(disk - 1, auxiliaryTower, targetTower, sourceTower);
        }
    }

    private string StackString(Stack<int> stack)
    {
        int[] array = new int[stack.Count];
        array = stack.ToArray();
        string arrayDisplay = "";
        for(int i = 0; i < array.Length; i++)
        {
            arrayDisplay += array[i].ToString() + " < ";
        }
        return arrayDisplay;
    }
}
