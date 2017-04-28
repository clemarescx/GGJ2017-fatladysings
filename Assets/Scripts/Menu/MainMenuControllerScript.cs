using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControllerScript : MonoBehaviour {

    #region Fields

    public string StartSceneName;
    public GameObject 
        /*Camera position objects*/
        MainPositionObject, 
        OptionsPositionObject,
        ControlsPositionObject,
        AboutPositionObject, 
        CreditsPositionObject,

        /*Camera offset position objects*/
        MainOffsetFromOptions, 
        MainOffsetFromAbout, 
        OptionsOffsetFromMain, 
        OptionsOffsetFromControls, 
        ControlsOffset,
        AboutOffsetFromMain,
        AboutOffsetFromCredits,
        CreditsOffset;

    public Camera MainCamera;
    public float TransitionSpeed;

    private Vector3 
        /*Camera positions*/
        _mainPosition, 
        _optionsPosition, 
        _controlsPosition,
        _aboutPosition, 
        _creditsPosition,

        /*Camera offset positions*/
        _mainOffsetFromOptionsPosition, 
        _mainOffsetFromAboutPosition, 
        _optionsOffsetFromMainPosition, 
        _optionsOffsetFromControlsPosition, 
        _controlsOffsetPosition,
        _aboutOffsetFromMainPosition,
        _aboutOffsetFromCreditsPosition,
        _creditsOffsetPosition;

    private Coroutine _currentLerp;
    private string _previousScreen;

    #endregion Fields

    #region OtherMethods

    private void Start() {
        //Set position values
        _mainOffsetFromOptionsPosition = MainOffsetFromOptions.gameObject.transform.position;
        _mainOffsetFromAboutPosition = MainOffsetFromAbout.gameObject.transform.position;
        _optionsOffsetFromMainPosition = OptionsOffsetFromMain.gameObject.transform.position;
        _optionsOffsetFromControlsPosition = OptionsOffsetFromControls.gameObject.transform.position;
        _controlsOffsetPosition = ControlsOffset.gameObject.transform.position;
        _aboutOffsetFromMainPosition = AboutOffsetFromMain.gameObject.transform.position;
        _aboutOffsetFromCreditsPosition = AboutOffsetFromCredits.gameObject.transform.position;
        _creditsOffsetPosition = CreditsOffset.gameObject.transform.position;

        _mainPosition = MainPositionObject.transform.position;
        _optionsPosition = OptionsPositionObject.transform.position;
        _controlsPosition = ControlsPositionObject.transform.position;
        _aboutPosition = AboutPositionObject.transform.position;
        _creditsPosition = CreditsPositionObject.transform.position;

        _previousScreen = "Main";
    }

    /// <summary>
    /// Loads the next/first Scene in the game
    /// </summary>
	public void StartGame() {
        SceneManager.LoadScene(StartSceneName);
    }

    /// <summary>
    /// Exits application
    /// </summary>
    public void ExitGame() {
        Application.Quit();
    }


    #endregion

    #region CanvasChangerMethods

    /// <summary>
    /// Returns to main PauseCanvas
    /// </summary>
    public void Back() {
        print("Going Back!");
        if (_currentLerp != null)
            StopCoroutine(_currentLerp);

        if (_previousScreen.Contains("Options")) {
            _currentLerp = StartCoroutine(LerpMainCamera(_mainPosition, _mainOffsetFromOptionsPosition));
        }
        else if (_previousScreen.Contains("About")) {
            _currentLerp = StartCoroutine(LerpMainCamera(_mainPosition, _mainOffsetFromAboutPosition));
        }
        else {
            print("Something went wrong when transitioning to Main");
        }

        _previousScreen = "Main";
    }

    /// <summary>
    /// Lerps to Options PauseCanvas
    /// </summary>
    public void Options() {
        print("Opening Options!");
        if (_currentLerp != null)
            StopCoroutine(_currentLerp);

        if (_previousScreen.Contains("Main")) {
            _currentLerp = StartCoroutine(LerpMainCamera(_optionsPosition, _optionsOffsetFromMainPosition));
        }
        else if (_previousScreen.Contains("Controls")) {
            _currentLerp = StartCoroutine(LerpMainCamera(_optionsPosition, _optionsOffsetFromControlsPosition));
        }
        else {
            print("Something went wrong when transitioning to Options");
        }
        _previousScreen = "Options";
    }

    /// <summary>
    /// Lerps to control PauseCanvas
    /// </summary>
    public void Controls() {
        print("Opening Controls!");
        if (_currentLerp != null)
            StopCoroutine(_currentLerp);
        _currentLerp = StartCoroutine(LerpMainCamera(_controlsPosition, _controlsOffsetPosition));

        _previousScreen = "Controls";
    }

    /// <summary>
    /// Lerps to About PauseCanvas
    /// </summary>
    public void About() {
        print("Opening About!");
        if (_currentLerp != null)
            StopCoroutine(_currentLerp);

        if (_previousScreen.Contains("Main")) {
            _currentLerp = StartCoroutine(LerpMainCamera(_aboutPosition, _aboutOffsetFromMainPosition));
        }
        else if (_previousScreen.Contains("Credits")) {
            _currentLerp = StartCoroutine(LerpMainCamera(_aboutPosition, _aboutOffsetFromCreditsPosition));
        }
        else {
            print("Something went wrong when transitioning to About");
        }
        _previousScreen = "About";
    }

    /// <summary>
    /// Lerps to Credits PauseCanvas
    /// </summary>
    public void Credits() {
        print("Opening Credits!");
        if (_currentLerp != null)
            StopCoroutine(_currentLerp);
        _currentLerp = StartCoroutine(LerpMainCamera(_creditsPosition, _creditsOffsetPosition));

        _previousScreen = "Credits";
    }


    /// <summary>
    /// Lerps camera from offset to end position
    /// </summary>
    /// <param name="endPosition">Where The camera ends after Lerp</param>
    /// <param name="offsetPosition">Position to start Lerping camera from</param>
    /// <returns></returns>
    private IEnumerator LerpMainCamera(Vector3 endPosition, Vector3 offsetPosition) {
        float t = 0;

        //Translating camera to offset position
        MainCamera.transform.position = offsetPosition;

        //Lerp camera to final Position
        while (t < TransitionSpeed) {
            //rewrite if to check for an approximation instead of absolutes
            if (MainCamera.transform.position != endPosition) {
                MainCamera.transform.position = Vector3.Lerp(offsetPosition, endPosition, t);
                t += Time.deltaTime / TransitionSpeed;
                yield return null;
            }
            else {
                MainCamera.transform.position = endPosition;
                yield break;
            }
            offsetPosition = MainCamera.transform.position;
        }
    }


    #endregion CanvasChangerMethods
}