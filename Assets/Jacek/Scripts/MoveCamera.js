#pragma strict

public var speed : float = 0.001;
var target : Transform;
public var boundary : float = 5;

private var ScreenWidth : float;

  
 function Start () {
 	ScreenWidth = Screen.width;
  } 
  
 function Update () {
     if (Input.mousePosition.x > ScreenWidth - boundary) {
         transform.LookAt(target);
         transform.RotateAround(target.position, Vector3.up, 1 *speed);
     }
 if (Input.mousePosition.x < 0 + boundary) {
         transform.LookAt(target);
         transform.RotateAround(target.position, Vector3.down, 1 *speed);
     }
 }
