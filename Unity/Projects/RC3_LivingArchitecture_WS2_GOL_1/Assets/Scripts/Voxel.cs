using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Voxel : MonoBehaviour {

	// VARIABLES
    //state
	private int state = 0;
    //next state
    private int futureState = 0;
    //age
    private int age = 0;
    //holds reference to time end from CAGRID which is also the height
    private int timeEndReference;
    //material property block for setting material properties with renderer
    private MaterialPropertyBlock props;
    //the mesh renderer
    private new MeshRenderer renderer;
    //var stores my 3d address
	public Vector3 address;

    //The Mesh Filter takes a mesh from your assets and passes it to the Mesh Renderer for rendering on the screen
    //One Voxel can contain different meshes which are the representation of different types of voxels
    public MeshFilter type1Mesh, type2Mesh, type3Mesh;

    //variable to store a type for this voxel
	int type;

    // FUNCTIONS

	public void SetupVoxel(int i, int j, int k, int _type)
    {
        //set reference to time end 
        timeEndReference = GameObject.Find("Environment").GetComponent<Environment>().timeEnd;
        props = new MaterialPropertyBlock();
        renderer = gameObject.GetComponent<MeshRenderer>();
        //initially set to false
        renderer.enabled = false;
        //set my address as a vector
		address = new Vector3 (i,j,k);

        //gets the type of this voxel and sets the mesh filter by type - allows us to preload
        //different meshes and render a different mesh for different voxels based on the type
		type = _type;
		switch (type) {
		case 1:
			MeshFilter setMesh = gameObject.GetComponent<MeshFilter> ();
			setMesh = type1Mesh;
			break;
		case 2:
			MeshFilter setMesh2 = gameObject.GetComponent<MeshFilter> ();
			setMesh2 = type2Mesh;
			break;
		case 3:
			MeshFilter setMesh3 = gameObject.GetComponent<MeshFilter> ();
			setMesh3 = type3Mesh;
			break;	
		default:
			MeshFilter setMeshDefault = gameObject.GetComponent<MeshFilter> ();
			setMeshDefault = type3Mesh;
			break;
		}
    }

	// Update function
	public void UpdateVoxel () {
		// Set the future state
		state = futureState;
        // If voxel is alive update age
        if (state == 1)
        {
            age++;
        }
        // If voxel is death disable the game object mesh renderer and set age to zero
        if (state == 0)
        {
            age = 0;
        }
    }

    // Update the voxel display
    public void VoxelDisplay()
    {
        if (state == 1)
        {            
            // Remap the color to age
            Color ageColor = new Color(Remap(age, 0, timeEndReference, 0.0f, 1.0f), 0, 0, 1);
            props.SetColor("_Color", ageColor);
            // Updated the mesh renderer color
            renderer.enabled = true;
            renderer.SetPropertyBlock(props);
        }
        if(state == 0)
        {
            renderer.enabled = false;
        }
    }

	// Set the state of the voxel
	public void SetState(int _state){
		state = _state;
	}

	// Set the future state of the voxel
	public void SetFutureState(int _futureState){
		futureState = _futureState;
	}

    // Get the age of the voxel
	public void SetAge(int _age){
		age = _age;
	}

	// Get the state of the voxel
	public int GetState(){
		return state;
	}

	// Get the age of the voxel
	public int GetAge(){
		return age;
	}

    // Remap numbers - used here for getting a gradient of color across a range
    private float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    Mesh MeshVoxel1()
    {
        Mesh type1Mesh = new Mesh();

        var positions = new Vector3[4];
        positions[0] = new Vector3(0.0f, 0.0f, 0.0f);
        positions[1] = new Vector3(1.0f, 0.0f, 1.0f);
        positions[2] = new Vector3(1.0f, 1.0f, 0.0f);
        positions[3] = new Vector3(0.0f, 1.0f, 1.0f);

        int[] indices = new int[]
        {
            2,1,0,
            2,0,3,
            3,1,2,
            0,1,3

        };

        type1Mesh.vertices = positions;

        Color[] colorsT = new Color[4];
        colorsT[0] = new Color(1f, 0f, 0.5f, 1);
        colorsT[1] = new Color(1f, 1f, 0.5f, 1);
        colorsT[2] = new Color(0f, 1f, 0.5f, 1);
        colorsT[3] = new Color(0f, 0f, 0.5f, 1);

        type1Mesh.colors = colorsT;
        type1Mesh.SetTriangles(indices, 0);
       
        return type1Mesh;
    }

    Mesh MeshVoxel2()
    {
        Mesh type2Mesh = new Mesh();

        var positions = new Vector3[6];
        positions[0] = new Vector3(0.0f, 0.0f, 0.0f);
        positions[1] = new Vector3(1.0f, 0.0f, 1.0f);
        positions[2] = new Vector3(0.0f, 1.0f, 0.5f);
        positions[3] = new Vector3(0.5f, 1.0f, 0.0f);
        positions[4] = new Vector3(0.5f, 1.0f, 1.0f);
        positions[5] = new Vector3(1.0f, 1.0f, 0.5f);

        /*
        int[] indices = new int[]
        {
            0,1,5,3
            0,3,2
            0,1,4,2
            1,4,5

        };
        */

        type2Mesh.vertices = positions;

        Color[] colors = new Color[6];
        colors[0] = new Color(1f, 0f, 0.5f, 1);
        colors[1] = new Color(1f, 1f, 0.5f, 1);
        colors[2] = new Color(0f, 1f, 0.5f, 1);
        colors[3] = new Color(0f, 0f, 0.5f, 1);
        colors[4] = new Color(0.5f, 1f, 0.5f, 1);
        colors[5] = new Color(1f, 0f, 0.5f, 0);

        type2Mesh.colors = colors;
        //type2Mesh.SetTriangles(indices, 0);

        return type2Mesh;
    }

    Mesh MeshVoxel3()
    {
        Mesh type3Mesh = new Mesh();

        var positions = new Vector3[4];
        positions[0] = new Vector3(0.0f, 0.0f, 0.0f);
        positions[1] = new Vector3(1.0f, 0.0f, 1.0f);
        positions[2] = new Vector3(1.0f, 1.0f, 0.0f);
        positions[3] = new Vector3(0.0f, 1.0f, 1.0f);

        int[] indices = new int[]
        {
            2,1,0,
            2,0,3,
            3,1,2,
            0,1,3
        };

        type3Mesh.vertices = positions;

        Color[] colors = new Color[4];
        colors[0] = new Color(1f, 0f, 0.5f, 1);
        colors[1] = new Color(1f, 1f, 0.5f, 1);
        colors[2] = new Color(0f, 1f, 0.5f, 1);
        colors[3] = new Color(0f, 0f, 0.5f, 1);

        type3Mesh.colors = colors;
        type3Mesh.SetTriangles(indices, 0);

        return type3Mesh;
    }




}
