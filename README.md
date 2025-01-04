<div align="center">
  <a href="https://www.youtube.com/@EnKhayzo">
        <img src="https://img.shields.io/youtube/channel/subscribers/UCsQQB2f90XGFyr_qtNikbCw?logoColor=red&logo=youtube&style=for-the-badge"
            alt="Youtube"></a>
  <a href="https://discord.gg/nZDkBDbHjU">
        <img src="https://img.shields.io/discord/1324804381610213407?color=blue&labelColor=555555&label=&logo=discord&style=for-the-badge"
            alt="Chat on Discord"></a>
  <a href="https://x.com/EnKhayzo">
        <img src="https://img.shields.io/twitter/follow/EnKhayzo?logo=x&logoColor=black&style=for-the-badge"
            alt="X / Twitter"></a>
  <a href="https://bsky.app/profile/enkhayzo.bsky.social">
        <img src="https://img.shields.io/badge/-Bluesky-3686f7?logo=icloud&logoColor=white&style=for-the-badge"
            alt="Bluesky"></a>
</div>

# BLAMEScaleTest
A basic Unity demo/showcase of the scale of The City in the Manga BLAME!

<div align="center">
        <img src=".github/images/preview.png?raw=true"
            alt="Youtube">
</div>

# The City
For the size and shape of The City i went with old posts that i found scatterend around the web, mostly i was inspired by this [reddit post](https://www.reddit.com/r/Netsphere/comments/5myc28/the_size_of_the_city/) and the links that a now deleted user has posted which link to an old fanbase forum post. And [this](https://forums.spacebattles.com/threads/please-explain-the-city-blame-to-me.529507/) forum post.

Basing the scale on those sources, i went with ~1.6e+12 units (or, assuming 1000 units/km, 1.6 billion kilometers/994 million miles), which is slightly bigger than Jupiter's orbit.\
The spikes go up to 15 trillion km/9 trillion miles away, which would be the theoretical extreme outer edge of the Oort Cloud, 100.000 AU away.

All of the numbers could be wrong, i didn't look too much into the specific or accurate measurements; but they should be in the right ballpark.

# Solar System
BLAMEScaleTest features a full scale* Solar System (albeit with lackluster assets :P and a bunch of planets missing), using a simple Floating Origin system to enable player movement at trillions+ units/second.

*the actual unit values are divided by 10 (so technically from Unity's perspective the solar system is 1/10th of its original size); however i compensate for this by decreasing the speed of the player by a factor of 10 and doing the same for the near plane of the first Camera. This is because at a scale of >10 billion units the lighting system breaks (all lighting-based assets larger than that seem to go black).

# Camera Setup
In order to have a near infinite draw distance (almost as much as a float can handle, up to 1e+29 units distance) the scene is rendered by 3 cameras at the same time, each with an increasing near-far plane;\
this is of course kind of a hack, so you'll notice that for example the labels being displayed on the various objects at times disappear and reappear, this is caused by the current camera's far plane "overlapping" with the next camera's near plane.

# Saved Preferences
BLAMEScaleTest saves a couple settings when you exit the game: specifically the mouse sensitivity and field of view you last set (Unity seems to save some more by default, like screen resolution and the like).\
The values are saved by Unity's default Preferences System, which saves settings in the Registry here: Computer\HKEY_CURRENT_USER\Software\DefaultCompany\BLAMEScaleTest.\
If you want to reset or delete the preferences go into regedit, into the path pasted above and delete the BLAMEScaleTest folder.

# Input/KeyBinds
I'll paste the contents of the Helper Dialog i put into the demo itself:\
H - Toggle This Dialog\
R - Reset Position\
Z - Toggle Camera Control\
WASD - Move\
Space - Go Up\
C/LCtrl - Go Down\
Q/E - Roll\
Mouse Wheel - Increase/Decrease speed\
LShift+Mouse Wheel - Increase/Decrease Mouse Sensitivity\
T - Toggle Size Labels\
LShift+Alt - Increase/Decrease Field Of View\
V - Toggle Universe Size Sphere\
Esc - Quit

# Why Unity?
This is actually a pretty old project (4+ years old) that i revisited and modified to have The City in it, so the main reason is that the project was already in Unity and i'm too lazy to rewrite it in Godot or UE (and unfortunately i'm the most familiar with Unity out of the bunch).
