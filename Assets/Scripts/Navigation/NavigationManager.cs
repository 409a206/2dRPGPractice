using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NavigationManager
{
    private static string PreviousLocation;

    public static Dictionary<string,Route> RouteInformation = 
        new Dictionary<string, Route>() {
            {"World", new Route{RouteDescription = "The big bad world", CanTravel = true}
            },
            {"Cave01", new Route{RouteDescription = "The deep dark cave", CanTravel = false}
            },
            {"Home", new Route{
                RouteDescription = "Home Sweet Home", CanTravel = true}
            },
            {"Kirkidw", new Route{
                RouteDescription = "the grand city of Kirkidw",
                CanTravel = true}},
            { "Battle", new Route { CanTravel = true}},
            {"Shop", new Route{CanTravel = true}},
        };
    
    public struct Route {
        public string RouteDescription;
        public bool CanTravel;
    }

    public static string GetRouteInfo(string destination) {
        return RouteInformation.ContainsKey(destination) ? RouteInformation[destination].RouteDescription : null;
    }

    public static bool CanNavigate(string destination) {
        return RouteInformation.ContainsKey(destination) ? 
        RouteInformation[destination].CanTravel : 
        false;
    }

    public static void NavigateTo(string destination) {
        PreviousLocation = Application.loadedLevelName;

        if(destination == "Home") {
            GameState.PlayerReturningHome = false;
        }
        GameState.SaveState();
        FadeInOutManager.FadeToLevel(
            destination,
            2f,
            2f,
            Color.black
        );
    }

    public static void GoBack() {
        var backlocation = PreviousLocation;
        PreviousLocation = Application.loadedLevelName;
        GameState.SaveState();
        FadeInOutManager.FadeToLevel(backlocation, 0.1f, 0.1f, Color.black);
    }
}
