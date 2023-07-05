using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;


public class Authentication : MonoBehaviour
{

    [SerializeField] TitleUIManager titleUIManager;
    public async void OnClickAnonymousLoginButton() => await SignInAnonymouslyAsync();
    public async void OnClickFacebookLoginButton() => await SignInAnonymouslyAsync();
    public async void OnClickGoogleLoginButton() => await SignInWithGoogleAsync(AuthenticationService.Instance.AccessToken);
    

    private void Awake() {
        titleUIManager = FindObjectOfType<TitleUIManager>();
    }
    async void Start()
    {
        
        //비동기로 유니티서비스 Init(초기화작업)
        await UnityServices.InitializeAsync();
        // 유니티서비스 상태 확인
        Debug.Log("UnityServicesState : " + UnityServices.State);
        // 로그인 토큰이 존재하는지 확인
        Debug.Log("Token Exitst : " + AuthenticationService.Instance.SessionTokenExists);
        Debug.Log("Token : " + AuthenticationService.Instance.AccessToken.ToString());

        
    }

    void SetupEvents(){
        // Action에 의해 토큰이 존재한다면 이벤트핸들러를 통해 실행됨
        // 즉, SignedIn은 이벤트가 발생할 때 실행되는 콜백 함수이며, 람다식을통해 이 이벤트가 발생했을때, 어떻게 작동할지 이벤트핸들러를 정의합니다. 
        AuthenticationService.Instance.SignedIn += () => {
            //플레이어 아이디 확인                
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");
            //플레이어 접속토큰 확인
            Debug.Log($"Access Token: {AuthenticationService.Instance.AccessToken}");
        };
        // 토큰 로그인이 실패했을때 대응하는 코드
        AuthenticationService.Instance.SignInFailed += errorResponse =>{
            Debug.LogError($"Sign in failed with error code: {errorResponse.ErrorCode}");
        };
        AuthenticationService.Instance.SignedOut += () => {
            Debug.Log("Player signed out");
        };
        AuthenticationService.Instance.Expired += () => {
            Debug.Log("Player session could not be refreshed and expired.");
        };
    }

    void InitializePlayCamesLogin(){
    
    }
    
    async Task SignInAnonymouslyAsync()
{
    try
    {
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        Debug.Log("Sign in anonymously succeeded!");
        
        // Shows how to get the playerID
        Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}"); 
        titleUIManager.PlayGame();

    }
    catch (AuthenticationException ex)
    {
        // Compare error code to AuthenticationErrorCodes
        // Notify the player with the proper error message
        Debug.LogException(ex);
    }
    catch (RequestFailedException ex)
    {
        // Compare error code to CommonErrorCodes
        // Notify the player with the proper error message
        Debug.LogException(ex);
     }
}


    async Task SignInWithGoogleAsync(string idToken)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithGoogleAsync(idToken);
            Debug.Log("SignIn is successful.");
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
    }

}
