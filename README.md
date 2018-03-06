# Kai Evenson -  Rube Goldberg Project

This project is part of [Udacity](https://www.udacity.com "Udacity - Be in demand")'s [VR Developer Nanodegree](https://www.udacity.com/course/vr-developer-nanodegree--nd017).

## Versions
- Unity 2017.2.0f3
- Oculus VR Integration 1.18
- SteamVR Plugin 1.2.3

# Notes to reviewer
I built this project over the course of two weeks; I estimate it took me between 18-20 hours total.

I didn't like how the teleportation was handled with the SteamVR SDK during the lessons so a good chunk of that time was spend trying to find a better way to handle this for the Oculus Touch system, which is the only one I'm currently able to use for testing (or for work). For that reason, while I did import the SteamVR SDK into the project for level loading, I did not use the SteamVR camera rig or controllers. Instead, I used the Oculus VR system and a custom teleporter system that was designed for Oculus systems. This is what I'll be using for my own projects until I have a Vive to test with (so I can take advantage of the headset changing script provided in the lessons). I hope this is acceptable. The SteamVR requirement seemed a bit arbitrary.

My favorite part of this project was getting more scripting practice in--it wasn't necessarily the most enjoyable (often frustrating), but I definitely got a lot out of it. In previous projects, I felt that most of the scripts were done (or mostly done) for us, but I wrote quite a bit of code for this project, which was definitely good practice for me. It was also really fun to finally incorporate controllers and more interaction into my project. I'm not studying to become a game developer but the mechanics of this game (interacting w/ various objects), will definitely come in handy for me.  

The most challenging part was just trying to fit everything in in the provided timeframe--it was a pretty big project.

Thanks for your time,
Kai


## Credits
- OVR SDK
- SteamVR SDK
- custom Arc Teleporter for Oculus Touch by [Gabor Szauer](https://developer.oculus.com/blog/teleport-curves-with-the-gear-vr-controller/)
- Rocket Pack by AurnSky
