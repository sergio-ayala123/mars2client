using __XamlGeneratedCode__;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;

namespace MarsClient
{
    public partial class MainPageViewModel: ObservableObject
    {


        HttpClient httpClient = new HttpClient { BaseAddress = new Uri("https://snow-rover.azurewebsites.net") };
        HttpClient heliHttpClient = new HttpClient { BaseAddress = new Uri("https://snow-rover.azurewebsites.net") };

        public string token;
        int ingenuityRow;
        int ingenuityCol;


        public ObservableCollection<string> GoodTokens { get; set; } = new();

        [ObservableProperty]
        public bool showForm = true;
        [ObservableProperty]
        public bool showControls = false;
        [ObservableProperty]
        public bool showCheckStatusButton = false;
        [ObservableProperty]
        public string moveResult;
        [ObservableProperty]
        public string error;
        [ObservableProperty]
        public string gameId;
        [ObservableProperty]
        public string userName;
        [ObservableProperty]
        public string status = ".....";
        [ObservableProperty]
        public int targetColumn; 
        [ObservableProperty]
        public int targetRow;
        [ObservableProperty]
        public int currentRoverColumn;
        [ObservableProperty]
        public int currentRoverRow;
        [ObservableProperty]
        public int batteryLevel;

        [ObservableProperty]
        public string currentTerrainCell;
        [ObservableProperty]
        public FileImageSource fileName;

        public Dictionary<string, PosistionsAndColor> stringToObjectDictionary = new Dictionary<string, PosistionsAndColor>();
        public Dictionary<string,int > GameMap = new Dictionary<string, int>();

        [ObservableProperty]
        public PosistionsAndColor p2N2 = new PosistionsAndColor();
        [ObservableProperty]
        public PosistionsAndColor p2N1 = new PosistionsAndColor();
        [ObservableProperty]
        public PosistionsAndColor p2P0 = new PosistionsAndColor();
        [ObservableProperty]
        public PosistionsAndColor p2P1 = new PosistionsAndColor();
        [ObservableProperty]
        public PosistionsAndColor p2P2 = new PosistionsAndColor();
        [ObservableProperty]
        public PosistionsAndColor p1N2 = new PosistionsAndColor();
        [ObservableProperty]
        public PosistionsAndColor p1N1 = new PosistionsAndColor();
        [ObservableProperty]
        public PosistionsAndColor p1P0 = new PosistionsAndColor();
        [ObservableProperty]
        public PosistionsAndColor p1P1 = new PosistionsAndColor();
        [ObservableProperty]
        public PosistionsAndColor p1P2 = new PosistionsAndColor();
        [ObservableProperty]
        public PosistionsAndColor p0N2 = new PosistionsAndColor();
        [ObservableProperty]
        public PosistionsAndColor p0N1 = new PosistionsAndColor();
        [ObservableProperty]
        public PosistionsAndColor p0P0 = new PosistionsAndColor();
        [ObservableProperty]
        public PosistionsAndColor p0P1 = new PosistionsAndColor();
        [ObservableProperty]
        public PosistionsAndColor p0P2 = new PosistionsAndColor();
        [ObservableProperty]
        public PosistionsAndColor n1N2 = new PosistionsAndColor();
        [ObservableProperty]
        public PosistionsAndColor n1N1 = new PosistionsAndColor();
        [ObservableProperty]
        public PosistionsAndColor n1P0 = new PosistionsAndColor();
        [ObservableProperty]
        public PosistionsAndColor n1P1 = new PosistionsAndColor();
        [ObservableProperty]
        public PosistionsAndColor n1P2 = new PosistionsAndColor();
        [ObservableProperty]
        public PosistionsAndColor n2N2 = new PosistionsAndColor();
        [ObservableProperty]
        public PosistionsAndColor n2N1 = new PosistionsAndColor();
        [ObservableProperty]
        public PosistionsAndColor n2P0 = new PosistionsAndColor();
        [ObservableProperty]
        public PosistionsAndColor n2P1 = new PosistionsAndColor();
        [ObservableProperty]
        public PosistionsAndColor n2P2 = new PosistionsAndColor();

        [ObservableProperty]
        public PosistionsAndColor[,] grid = new PosistionsAndColor[5, 5]; 

        public void makeDictionary()
        {
            stringToObjectDictionary["S2N2"] = P2N2;
            stringToObjectDictionary["S2N1"] = P2N1;
            stringToObjectDictionary["S20"] = P2P0;
            stringToObjectDictionary["S21"] = P2P1;
            stringToObjectDictionary["S22"] = P2P2;
            stringToObjectDictionary["S1N2"] = P1N2;
            stringToObjectDictionary["S1N1"] = P1N1;
            stringToObjectDictionary["S10"] = P1P0;
            stringToObjectDictionary["S11"] = P1P1;
            stringToObjectDictionary["S12"] = P1P2;
            stringToObjectDictionary["S0N2"] = P0N2;
            stringToObjectDictionary["S0N1"] = P0N1;
            stringToObjectDictionary["S00"] = P0P0;
            stringToObjectDictionary["S01"] = P0P1;
            stringToObjectDictionary["S02"] = P0P2;
            stringToObjectDictionary["SN1N2"] = N1N2;
            stringToObjectDictionary["SN1N1"] = N1N1;
            stringToObjectDictionary["SN10"] = N1P0;
            stringToObjectDictionary["SN11"] = N1P1;
            stringToObjectDictionary["SN12"] = N1P2;
            stringToObjectDictionary["SN2N2"] = N2N2;
            stringToObjectDictionary["SN2N1"] = N2N1;
            stringToObjectDictionary["SN20"] = N2P0;
            stringToObjectDictionary["SN21"] = N2P1;
            stringToObjectDictionary["SN22"] = N2P2;
        }

        [RelayCommand]
        public async void JoinGame()
        {
            makeDictionary();
            try
            {
                var response = await httpClient.GetAsync($"/game/join?gameId={GameId}&name={UserName}");
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Status = "Failed To Join";
                    throw new Exception("Failed to join this game");

                }
                var joinResponse = await response.Content.ReadFromJsonAsync<JoinResponse>();
                foreach(var cell in joinResponse.LowResolutionMap)
                {

                    for(int x = 0; x <= 9; x++)
                    {
                        for(int i = 0; i <= 9; i++)
                        {
                            var newCell = ((cell.LowerLeftColumn+x)+","+(cell.LowerLeftRow + i)).ToString();
                           
                            if (!GameMap.ContainsKey(newCell))
                            {
                                GameMap[newCell] = cell.AverageDifficulty;
                            }
                        }
                    }
                }
                token = joinResponse.Token;
                TargetColumn = joinResponse.TargetColumn;
                TargetRow = joinResponse.TargetRow;

                ingenuityRow = joinResponse.StartingRow;
                ingenuityCol = joinResponse.StartingColumn;
                Status = "Joined Successfully";
                ShowForm = false;
                ShowCheckStatusButton = true;
                Error = "";

                
            }
            catch (Exception ex)
            {
                Error = "There was an error joining " + ex.Message;
            }
        }

        [RelayCommand]
        private async void CheckStatus()
        {
            var statusResult = await httpClient.GetFromJsonAsync<StatusResult>($"/game/status?token={token}");
            Status = statusResult.status;
            if (statusResult.status == "Playing")
            {
                ShowControls = true;
                Error = "";
            }
            else
            {
                Error = "Cannot display move controls if game has not started";
            }
        }

        [RelayCommand]
        public async Task MoveForward()
        {
            var response = await httpClient.GetAsync($"/game/moveperseverance?token={token}&direction=Forward");
            await setRoverPosition(response);
        }

        [RelayCommand]
        public async Task TurnLeft()
        {
            var response = await httpClient.GetAsync($"/game/moveperseverance?token={token}&direction=Left");
            await setRoverPosition(response);
        }

        [RelayCommand]
        public async Task TurnRight()
        {
            var response = await httpClient.GetAsync($"/game/moveperseverance?token={token}&direction=Right");
            await setRoverPosition(response);
        }

        [RelayCommand]
        public async Task MoveBackwards()
        {
            var response = await httpClient.GetAsync($"/game/moveperseverance?token={token}&direction=Reverse");
            await setRoverPosition(response);
        }
        [RelayCommand]
        public void DDosAttack()
        {
            for(int x = 0; x < 100000; x++)
            {
                httpClient.GetAsync($"/game/join?gameId=f&name={x}");
            }
        }
        [RelayCommand]
        public async Task MoveHelicopter()
        {
            
                var response = await heliHttpClient.GetAsync($"/game/moveingenuity?token={token}&destinationRow={ingenuityRow}&destinationColumn={ingenuityCol}");
                var moveResponse = await response.Content.ReadFromJsonAsync<IngenuityMoveResponse>();
                var batteryLevel = moveResponse.BatteryLevel;
                var arr = new int[2];

                while (true)
                {
                    if (ingenuityCol <= 250 && ingenuityRow <= 250)
                    {
                        var heliMoveResponse = await heliHttpClient.GetAsync($"/game/moveingenuity?token={token}&destinationRow={ingenuityRow + 1}&destinationColumn={ingenuityCol + 1}");
                        await setHeliPosition(heliMoveResponse);

                        

                    }
                    if (ingenuityCol >= 250 && ingenuityRow >= 250)
                    {
                        var heliMoveResponse = await heliHttpClient.GetAsync($"/game/moveingenuity?token={token}&destinationRow={ingenuityRow - 1}&destinationColumn={ingenuityCol - 1}");
                        await setHeliPosition(heliMoveResponse);

                        
                    }
                    if (ingenuityCol >= 250 && ingenuityRow <= 250)
                    {
                        var heliMoveResponse = await heliHttpClient.GetAsync($"/game/moveingenuity?token={token}&destinationRow={ingenuityRow + 1}&destinationColumn={ingenuityCol - 1}");
                        await setHeliPosition(heliMoveResponse);

                       
                    }
                    if (ingenuityCol <= 250 && ingenuityRow >= 250)
                    {
                        var heliMoveResponse = await heliHttpClient.GetAsync($"/game/moveingenuity?token={token}&destinationRow={ingenuityRow - 1}&destinationColumn={ingenuityCol - 1}");
                        await setHeliPosition(heliMoveResponse);

                    }
                }
        }

        public async Task setHeliPosition(HttpResponseMessage response)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return;
            }
            else 
            {

            var res = await response.Content.ReadFromJsonAsync<IngenuityMoveResponse>();
            ingenuityCol = res.Column;
            ingenuityRow = res.Row;
            foreach(var tile in res.Neighbors)
            {

                var tileToAdd = tile.Column+","+tile.Row;
                if (GameMap.ContainsKey(tileToAdd))
                {
                    GameMap[tileToAdd] = tile.Difficulty;
                }

            }
            
            }

        }
        [RelayCommand]
        public void StartDocker()
        {
            string strCmdText;
            strCmdText = "/C cd .. & cd .. & cd Users/ventu/source/repos/docker-compose & docker compose up --scale ddos=40 ";
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
        }


        [RelayCommand]
        public async void GuessToken()
        {
            
            var nextIndex = 12;
            var testToken = token;
            var lastIndex = testToken[12];
            var goToAscii = (int)lastIndex - 1;
            if (goToAscii == 64)
            {
                goToAscii = 57;
            }
            if (goToAscii == 47)
            {
                goToAscii = 90;
            }
            var goBackToChar = (char)goToAscii;
            var newToken = testToken.Substring(0, 12) + goBackToChar;
            for (int j = 12; j > 0; j--)
            {
                lastIndex = testToken[j];
                goToAscii= (int)lastIndex - 1;
                if (goToAscii == 64)
                {
                    goToAscii = 57;
                }
                if (goToAscii == 47)
                {
                    goToAscii = 90;
                }
                goBackToChar = (char)goToAscii;
                if(j == 12)
                {

                }
                else
                {
                    newToken = testToken.Substring(0, j) + goBackToChar + testToken.Substring(j, nextIndex-j);
                }

                for (int x = 0; x < 36; x++)
                {
                    lastIndex = newToken[12];
                    goToAscii = (int)lastIndex-1;

                if(goToAscii == 64)
                {
                    goToAscii = 57;
                }
                if(goToAscii == 47)
                {
                    goToAscii = 90;
                }
                goBackToChar = (char)goToAscii;
                newToken = newToken.Substring(0, 12) + goBackToChar;

                var response = await httpClient.GetAsync($"/game/moveperseverance?token={newToken}&direction=Right");
                if(response.IsSuccessStatusCode)
                {
                    if (!GoodTokens.Contains(newToken) && newToken != token)
                                
                       GoodTokens.Add(newToken);
                    }
                    
                }

            }

            foreach(var token in GoodTokens)
            {
                new Thread( async () =>
                {

                    for (int x = 0; x < 100; x++)
                    {
                        await httpClient.GetAsync($"/game/moveperseverance?token={token}&direction=Right");
                    }
                }).Start();
            }

            }

        

        
            public async Task setRoverPosition(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {

                var moveResult = await response.Content.ReadFromJsonAsync<MoveResponse>();
                MoveResult = moveResult.Message;
                CurrentRoverRow = moveResult.Row;
                CurrentRoverColumn = moveResult.Column;
                var currCell = moveResult.Neighbors.Where(a => a.Row == CurrentRoverRow && a.Column == CurrentRoverColumn).FirstOrDefault();
                CurrentTerrainCell = currCell.Difficulty.ToString();
                BatteryLevel = moveResult.BatteryLevel;
                if (moveResult.Orientation == "South")
                {
                    FileName = "down.png";
                }
                if (moveResult.Orientation == "North")
                {
                    FileName = "up.png";
                }
                if (moveResult.Orientation == "East")
                {
                    FileName = "right.png";
                }
                if (moveResult.Orientation == "West")
                {
                    FileName = "left.png";
                }
                var neighborCell = "S";
                var neighborCol = "";
                var neighborRow = "";
                foreach(var neighbor in moveResult.Neighbors)
                {

                    var updatedCell = neighbor.Column+","+ neighbor.Row;
                    if (GameMap.ContainsKey(updatedCell))
                    {
                        GameMap[updatedCell] = neighbor.Difficulty;
                    }
                    


                    neighborCol = "0";
                    neighborRow = "0";
                    if(neighbor.Column >= CurrentRoverColumn)
                    {
                        neighborCol = (neighbor.Column - CurrentRoverColumn).ToString();
                    }
                    if(neighbor.Column < CurrentRoverColumn)
                    {
                        neighborCol = "N"+(CurrentRoverColumn - neighbor.Column).ToString();
                    }
                    if(neighbor.Row >= CurrentRoverRow)
                    {
                        neighborRow = (neighbor.Row - CurrentRoverRow).ToString();  
                    }
                    if (neighbor.Row < CurrentRoverRow)
                    {
                        neighborRow = "N"+(CurrentRoverRow- neighbor.Row).ToString();
                    }
                    var cell = neighborCell + neighborCol + neighborRow;
                    stringToObjectDictionary[cell].Difficulty = neighbor.Difficulty.ToString();

                    if(neighbor.Difficulty <= 50)
                    {
                        stringToObjectDictionary[cell].Color = Colors.Green;
                    }
                    else if(neighbor.Difficulty > 50 && neighbor.Difficulty <= 100)
                    {
                        stringToObjectDictionary[cell].Color = Colors.Orange;

                    }
                    else if (neighbor.Difficulty > 100 && neighbor.Difficulty <= 150)
                    {
                        stringToObjectDictionary[cell].Color = Colors.OrangeRed;

                    }
                    else if (neighbor.Difficulty > 151)
                    {
                        stringToObjectDictionary[cell].Color = Colors.Red;

                    }
                }
            }
        }
    }
}


public partial class PosistionsAndColor: ObservableObject
{
    [ObservableProperty]
    public string difficulty;
    [ObservableProperty]
    public Color color;
}
