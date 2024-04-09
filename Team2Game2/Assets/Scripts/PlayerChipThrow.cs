using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerChipThrow : MonoBehaviour
{
    public Camera cam;
    public GameObject chipPrefab;
    public Transform chipSpawn;

    Vector3 MousePos;
    Vector3 MouseWorldPos;

    // Update is called once per frame
    void Update()
    {
        //----- Set MouseWorldPos - 2d mouse pos -> world pos... Z-pos = player Z-pos -----
        //Note: only works when camera is targeting player
        {
            //Initial position calc: this will give a good plane, but it will be slanted due to the camera angle
            float z = Vector3.Distance(gameObject.transform.position, cam.transform.position);
            MouseWorldPos = cam.ScreenToWorldPoint(new Vector3(MousePos.x, MousePos.y, z));

            //Calculating camera angle fixes
            float camXRot = cam.transform.rotation.eulerAngles.x;
            float yoffset = MouseWorldPos.y - gameObject.transform.position.y;
            float zoffset = Mathf.Sin(Mathf.Deg2Rad * camXRot) * yoffset;

            //Recaculating world position with fixed angle
            z = Vector3.Distance(gameObject.transform.position, cam.transform.position) - zoffset;
            MouseWorldPos = cam.ScreenToWorldPoint(new Vector3(MousePos.x, MousePos.y, z));

            //Basically rounding the z pos to player, previous angle fix makes this much less noticable
            MouseWorldPos.z = gameObject.transform.position.z;
        }
        //End setting MouseWorldPos
    }

    public void SetMousePosition(InputAction.CallbackContext ctx)
    {
        MousePos = ctx.ReadValue<Vector2>();
    }
    public void MouseClicked(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && canThrow)
        {
            //Create chip
            GameObject chip = Instantiate(chipPrefab,chipSpawn.position,Quaternion.identity);

            //Throw chip
            Vector3 _force = MouseWorldPos- gameObject.transform.position;
            chip.GetComponent<Rigidbody>().AddForce(_force, ForceMode.Impulse);

            StartCoroutine(RemoveChip(chip));
            StartCoroutine(ChipThrowCooldown());
        }
    }
    private void OnDrawGizmos() //Debug for mouse world position
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(MouseWorldPos, 0.25f);
    }

    IEnumerator RemoveChip(GameObject chip)
    {
        yield return new WaitForSeconds(5f);
        if (chip != null) Destroy(chip);
    }
    float cooldown = 0.25f;
    bool canThrow = true;
    IEnumerator ChipThrowCooldown()
    {
        canThrow = false;
        yield return new WaitForSeconds(cooldown);
        canThrow = true;
    }
}
