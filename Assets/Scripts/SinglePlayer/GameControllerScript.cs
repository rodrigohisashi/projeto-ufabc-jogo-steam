using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour
{
    public const int columns = 5;
    public const int rows = 2;
    public const float Xspace = 4f;
    public const float Yspace = -5f;
    public GameObject menuObject; // Assign the GameObject whose alpha you want to access
    public GameObject parabensObject;
    private bool terminou = true;
    SpriteRenderer sRMenu;
    SpriteRenderer sRParabens;
    



    private List<int> cardIDs = new List<int>();
    [SerializeField] private MainImageScript startObject;
    [SerializeField] private Sprite[] images;
    [SerializeField] private Sprite[] ImagemArea;

    //embaralha as posições para gera, posições aleatorias
    private int[] Randomiser(int[] locations)
    {
        int[] array = locations.Clone() as int[];
        for(int i=0; i < array.Length; i++)
        {
            int newArray = array[i];
            int j = Random.Range(i, array.Length);
            array[i] = array[j];
            array[j] = newArray;
        }
        return array;
    }

    private void Start()
    {
         
        // Get the SpriteRenderer component of the targetObject
        sRMenu = menuObject.GetComponent<SpriteRenderer>();
        menuObject.SetActive(false);

        sRParabens = parabensObject.GetComponent<SpriteRenderer>();
        parabensObject.SetActive(false);
        
        //Pega 5 valores de imagens
        int [] test = {0,1,2,3,4};
        test = Randomiser(test);
        
        //Selecionar os primeiros 4
        int[] vetorOriginal = new int[5];
        
        for (int i = 0;i < 5; i++){
            vetorOriginal[i] = test[i];
        }
  
        
        int tamanhoDesejado = vetorOriginal.Length * 2;
        int[] vetorDuplicado = new int[tamanhoDesejado];

        for (int i = 0; i < vetorOriginal.Length; i++)
        {
             vetorDuplicado[i * 2] = vetorOriginal[i];
            vetorDuplicado[i * 2 + 1] = vetorOriginal[i];
        }

         


        int[] locations = vetorDuplicado;
        locations = Randomiser(locations);

        Vector3 startPosition = startObject.transform.position;

        for(int i = 0; i < columns; i++)
        {
            for(int j = 0; j < rows; j++)
            {
                MainImageScript gameImage;
                if(i == 0 && j == 0)
                {
                    gameImage = startObject;
                }
                else
                {
                    gameImage = Instantiate(startObject) as MainImageScript;
                }
                

                int index = j * columns + i;
                int id = locations[index];
                
                
               
                Sprite imagemDaCarta = images[id];
                Sprite imagemDaArea = ImagemArea[id];
                
                if(cardIDs.Contains(id)){
                    gameImage.ChangeSprites(id, imagemDaArea);
                    //print("Carta");
                }
                else {
                    gameImage.ChangeSprites(id,imagemDaCarta );
                    cardIDs.Add(id);
                    //print("Area");
                }

                float positionX = (Xspace * i) + startPosition.x;
                float positionY = (Yspace * j) + startPosition.y;

                gameImage.transform.position = new Vector3(positionX, positionY, startPosition.z); 
            }
        }
    }

    private MainImageScript firstOpen;
    private MainImageScript secondOpen;

    private int score = 0;
    private int attempts = 0;

    [SerializeField] private TextMesh scoreText;
    [SerializeField] private TextMesh attemptsText;

    public bool canOpen
    {
        get { return secondOpen == null; }
    }

    public void imageOpened(MainImageScript startObject)
    {
        if(firstOpen == null)
        {
            firstOpen = startObject;
        }
        else
        {
            secondOpen = startObject;
            StartCoroutine(CheckGuessed());
        }
    }

    private IEnumerator CheckGuessed()
    {
        if (firstOpen.spriteId == secondOpen.spriteId) // Compares the two objects
        {
            score++; // Add score
            scoreText.text = "Pontos: " + score;
        }
        else
        {
            yield return new WaitForSeconds(0.5f); // Start timer

            firstOpen.Close();
            secondOpen.Close();
        }

        attempts++;
        attemptsText.text = "Tentativas: " + attempts;

        firstOpen = null;
        secondOpen = null;
    }

    public void Restart()
    {
        SceneManager.LoadScene("SinglePlayer");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    // Update is called once per frame
    void Update()
    {
        if (score == 5 && terminou){
            print ("2");
            Aparece();
            terminou=false;
        } 
    }

    private void Aparece(){
        // Ensure the targetObject is assigned
        if (menuObject != null)
        {
            

            // Check if the spriteRenderer is not null
            if (sRMenu != null)
            {
                // Activate the targetObject
                menuObject.SetActive(true);
                parabensObject.SetActive(true);
                //spriteRenderer = targetObject.GetComponent<SpriteRenderer>();
                

                 // Set the alpha value of the SpriteRenderer's color to 1 (255)
                Color currentColor = sRMenu.color;
                Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, 1f);
                sRMenu.color = newColor;

                Color currentColor2 = sRParabens.color;
                Color newColor2 = new Color(currentColor2.r, currentColor2.g, currentColor2.b, 1f);
                sRParabens.color = newColor2;

                playWinAudio();

                Debug.Log("Alpha value of the SpriteRenderer's color set to 1 (255).");
            }
            else
            {
                Debug.LogError("SpriteRenderer component not found on the target object.");
            }
        }
        else
        {
            Debug.LogError("Target object not assigned. Please assign the target GameObject in the inspector.");
        }
    }  

    void playWinAudio() {
        GameObject.Find("soundWin").GetComponent<soundWin>().playWin();
    }  
}
