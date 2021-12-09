# Aquariox-MKX2000
A relaxing fish tank aquarium simulator.
                                                                                                                                                                     

	      .o.                                                  o8o                        
	     .888.                                                 `"'                        
	    .8"888.      .ooooo oo oooo  oooo   .oooo.   oooo d8b oooo   .ooooo.  oooo    ooo 
	   .8' `888.    d88' `888  `888  `888  `P  )88b  `888""8P `888  d88' `88b  `88b..8P'  
	  .88ooo8888.   888   888   888   888   .oP"888   888      888  888   888    Y888'    
	 .8'     `888.  888   888   888   888  d8(  888   888      888  888   888  .o8"'88b   
	o88o     o8888o `V8bod888   `V88V"V8P' `Y888""8o d888b    o888o `Y8bod8P' o88'   888o 
	                      888.                                                            
	                      8P'                                                             
	                      "                                                               
	ooo        ooooo oooo    oooo     ooooooo  ooooo       .oooo.   oooo    oooo          
	`88.       .888' `888   .8P'       `8888    d8'      .dP""Y88b  `888   .8P'           
	 888b     d'888   888  d8'           Y888..8P              ]8P'  888  d8'             
	 8 Y88. .P  888   88888[              `8888'             .d8P'   88888[               
	 8  `888'   888   888`88b.           .8PY888.          .dP'      888`88b.             
	 8    Y     888   888  `88b.  .o.   d8'  `888b   .o. .oP     .o  888  `88b.           
	o8o        o888o o888o  o888o Y8P o888o  o88888o Y8P 8888888888 o888o  o888o          
                                                                                      
	
	made by 
	 _                            
	| \ _ __  o  _  |             
	|_/(_|| | | (/_ |             
                              
	|/ |_  _  _| _  __ _ |_ __  o 
	|\ | |(_)(_|(_| | (_|| |||| | 


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

## Patterns

### Singleton
* Used in "FishManager.cs" in class "FishManager"
	* Used it so the fish could get a list of behaviours from the FishManager
* Used in "CoinManager.cs" in "GoldFish.cs"/"Perch.cs"/"Mullet.cs"
	* Used to spawn coins from the different fishtypes

### Dependency Injection
* Used in "FishBase.cs", in class "FishBase"
	* I store the fish data / stats in scriptable objects, and inject them into fish when created as a base. The values are slightly randomized before getting set.

### Composition
* Used in "FishBase.cs" in class "FishBase" 
	* Each fish has an array of behaviours it uses to perform the boid/flocking behaviour, these are divided up in separate components, and get applied on each "flocking-tick". This way I can easily add more behaviours later.

### Observer
* Used in "GameManager.cs"/"FishBase.cs"
	* Game manager counts a given amount of frames before firing off a "OnTick" event as well as a "OnSchoolingTick" event, these are spaced out over several frames to lighten the load and prevent spikes. (and to only run expensive operations every X frames)

### Object Pool
* Used in "CoinManager.cs"
	* The CoinManager gameobject has an Object Pool component, that has a flag for if the pool should expand or not.
		The coins are then initialized according to the coindroptable -- which represents the base droprate for all coins

