using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutGeneratorRooms : MonoBehaviour
{
    [SerializeField] int width = 64;
    [SerializeField] int length = 64;

    [SerializeField] int roomWidthMin = 3;
    [SerializeField] int roomWidthMax = 5;
    [SerializeField] int roomLengthMin = 3;
    [SerializeField] int roomLengthMax = 5;

    [SerializeField] GameObject levelLayoutDisplay; 

    System.Random random;

    [ContextMenu("Generate Level Layout")]
    public void GenerateLevel()
    {
        random = new System.Random();   //instantiate random variable
        var roomRect = GetStartRoomRect();  //create rectangle for first room
        Debug.Log(roomRect);     //log coordinates of the room
        DrawLayout(roomRect);
    }

    RectInt GetStartRoomRect()   //method that returns a rectangle
    {
        //get x coordinates for room
        int roomWidth = random.Next(roomWidthMin, roomWidthMax); //determine width of room using random width and length
        int availableWidthX = width / 2 - roomWidth;  //wiggle room available
        int randomX = random.Next(0, availableWidthX); //calculate roomX position in inner rectangle
        int roomX = randomX + (width / 4);  //calculate roomX position in level

        //get y coordinates for room
        int roomLength = random.Next(roomLengthMin, roomLengthMax);
        int availableLengthY = length / 2 - roomLength;
        int randomY = random.Next(0, availableLengthY);
        int roomY = randomY + (length / 4);

        //generate rectangle
        return new RectInt(roomX, roomY, roomWidth, roomLength);
    }

    void DrawLayout(RectInt roomCandidateRect = new RectInt())  //set empty rect as default
    {
        var renderer = levelLayoutDisplay.GetComponent<Renderer>();   //get renderer
        var layoutTexture = (Texture2D)renderer.sharedMaterial.mainTexture; //create variable for texture
        layoutTexture.Reinitialize(width, length);
        levelLayoutDisplay.transform.localScale = new Vector3(width, length, 1);
        layoutTexture.FillWithColor(Color.black);
        layoutTexture.DrawRectangle(roomCandidateRect, Color.cyan);
        layoutTexture.SaveAsset(); 
    }

}
