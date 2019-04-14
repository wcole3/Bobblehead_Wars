# Bobblehead_Wars
Slightly altered version of "Bobblehead Wars" from raywenderlich.com tutorial book 
"Unity Games by Tutorial" 3rd Ed. (two thumbs way up)

# BobbleHead Wars

##Changes from original tutorial

-- Added UI for health and enemies remaining

-- Added gameover panel to reset the game or exit (Only from build)

-- Allowed gun upgrade to spawn multiple times

-- Added free camera controls, controlled by mouse movement

-- Changed player movement from World space to local (Player) space

-- Animated all arena walls seperately

--Added navmesh obstacles to arena walls
 - Aliens now do not walk through walls (most of the time, they're tricky little buggers)
 
-- Altered wall collision to be between camera and wall instead of player and wall 
 - This was a result of allowing for camera rotation

-- Added a general Object Pooler
 - Items that are pooled: only projectiles currently
