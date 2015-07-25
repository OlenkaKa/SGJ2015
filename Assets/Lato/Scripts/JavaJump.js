var jumpH=1;
var isFalling=false;

function Update () {
	if(isFalling==false)
	{
		//rigidbody.velocity.y=jumpH;
		isFalling=true;
	}
}

function OnCollisionStay()
{
	isFalling=false;
}