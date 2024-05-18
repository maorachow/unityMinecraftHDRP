using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
public class pauseMenuUI : MonoBehaviour
{
    public static pauseMenuUI instance;
   
    public Button rebuildAllChunksButton;
    public Button SaveWorldButton;
    public Text viewRangeText;
    public Slider viewRangeSlider;
    public Slider graphicsQualitySlider;
    public Text graphicsQualityText;
    public PlayerMove player;
    public Button returnToMainMenuButton;
    public InputField resourcesDirectoryField;
    public Button loadResourceButton;
    public Text resourcePackLoadingStatsText;
  //  public Image normalMapImage;
    void Start()
    {
        instance=this;
        resourcePackLoadingStatsText=GameObject.Find("resourcepackloadingstatstext").GetComponent<Text>();
        resourcesDirectoryField=GameObject.Find("resourcedirectoryfield").GetComponent<InputField>();
    //    normalMapImage = GameObject.Find("normalmapimage").GetComponent<Image>();
        loadResourceButton=GameObject.Find("loadresourcepackbutton").GetComponent<Button>();
       TerrainTextureMipmapAdjusting. applyingTerrainNormal = Resources.Load<Texture2D>("Textures/terrainnormal");
        TerrainTextureMipmapAdjusting.SetTerrainNormalMipmap(out TerrainTextureMipmapAdjusting.applyingTerrainNormal);
     //   normalMapImage.sprite=  Sprite.Create(terrainNormal, new Rect(0, 0, terrainNormal.width, terrainNormal.height), Vector2.zero);
        player =GameObject.Find("player").GetComponent<PlayerMove>();
        graphicsQualitySlider=GameObject.Find("graphicsqualityslider").GetComponent<Slider>();
        graphicsQualityText=GameObject.Find("graphicsqualitytext").GetComponent<Text>();
        viewRangeSlider=GameObject.Find("viewrangeslider").GetComponent<Slider>();
        viewRangeText=GameObject.Find("viewrangetext").GetComponent<Text>();
        rebuildAllChunksButton=GameObject.Find("rebuildallchunksbutton").GetComponent<Button>();
        viewRangeSlider.onValueChanged.AddListener(ViewRangeSliderOnValueChanged);
        rebuildAllChunksButton.onClick.AddListener(RebuildAllChunksButtonOnClick);
        SaveWorldButton=GameObject.Find("saveworldbutton").GetComponent<Button>();
        SaveWorldButton.onClick.AddListener(SaveWorldButtonOnClick);
        returnToMainMenuButton=GameObject.Find("pausemainmenubutton").GetComponent<Button>();
        returnToMainMenuButton.onClick.AddListener(ReturnToMainMenuButtonOnClick);
        graphicsQualitySlider.onValueChanged.AddListener(GraphicsQualitySliderOnValueChanged);
        loadResourceButton.onClick.AddListener(LoadResourceButtonOnClick);
    }
    void GraphicsQualitySliderOnValueChanged(float f){
        Debug.Log(TerrainTextureMipmapAdjusting.applyingTerrainNormal.activeMipmapLimit);
        switch((int)graphicsQualitySlider.value){
            case 0:
            graphicsQualityText.text="Performant";
            QualitySettings.SetQualityLevel(0, true);
                RenderingQualityManager.instance.SetRenderingQuality((int)graphicsQualitySlider.value);
                VoxelWorld.chunkPrefab.GetComponent<MeshRenderer>().sharedMaterial.SetTexture("_NormalMap", null);
            break;
            case 1:
             graphicsQualityText.text="Balanced";
            QualitySettings.SetQualityLevel(1, true);
                RenderingQualityManager.instance.SetRenderingQuality((int)graphicsQualitySlider.value);
                VoxelWorld.chunkPrefab.GetComponent<MeshRenderer>().sharedMaterial.SetTexture("_NormalMap", null);
            break;
            case 2:
             graphicsQualityText.text="High Fidelity";
            QualitySettings.SetQualityLevel(2, true);
                RenderingQualityManager.instance.SetRenderingQuality((int)graphicsQualitySlider.value);
                VoxelWorld.chunkPrefab.GetComponent<MeshRenderer>().sharedMaterial.SetTexture("_NormalMap", TerrainTextureMipmapAdjusting.applyingTerrainNormal);
            break;
           
        }
    }
    void ViewRangeSliderOnValueChanged(float f){
        PlayerMove.viewRange=viewRangeSlider.value;
        viewRangeText.text=viewRangeSlider.value.ToString();

    }
    void ReturnToMainMenuButtonOnClick(){
        TerrainTextureMipmapAdjusting.ResetItemChunkTextures();
     //   WorldManager.isGoingToQuitGame=true;
     
        // WorldManager.DestroyAllChunks();
          SaveWorldButtonOnClick();


    }
     void SaveWorldButtonOnClick(){
        //   player.SavePlayerData();
        //  Chunk.SaveWorldData();
        //    EntityBeh.SaveWorldEntityData();
        //   ItemEntityBeh.SaveWorldItemEntityData();
        SceneManagementHelper.QuitToMainMenu();
    }
    void RebuildAllChunksButtonOnClick(){
        foreach(KeyValuePair<Vector2Int,Chunk> kvp in VoxelWorld.currentWorld.chunks){
            kvp.Value.isChunkMapUpdated=true;
        }
    }
    void LoadResourceButtonOnClick(){
        string resourcePackRootPath=resourcesDirectoryField.text;
        bool isLoadingSuccessful=FileAssetLoaderBeh.instance.LoadBlockNameDic(resourcePackRootPath+"/blockname.dat");
        isLoadingSuccessful=FileAssetLoaderBeh.instance.LoadChunkBlockInfo(resourcePackRootPath+"/blockterraininfo.dat");
        isLoadingSuccessful=FileAssetLoaderBeh.instance.LoadItemBlockInfo(resourcePackRootPath+"/itemblockinfo.dat");
        isLoadingSuccessful=FileAssetLoaderBeh.instance.LoadBlockAudio(resourcePackRootPath+"/audioab.dat");
        isLoadingSuccessful=FileAssetLoaderBeh.instance.LoadBlockTexture(resourcePackRootPath+"/textureab.dat");
        if(isLoadingSuccessful==true){
            resourcePackLoadingStatsText.text="Successfully loaded a resources pack";
        }else{
            resourcePackLoadingStatsText.text="Resources pack loading failed";
        }
    }
    void OnApplicationQuit(){
        TerrainTextureMipmapAdjusting.ResetItemChunkTextures();
        
    }
}
