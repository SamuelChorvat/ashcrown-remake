# Ashcrown Remake
[![Run Tests](https://github.com/SamuelChorvat/ashcrown-remake/actions/workflows/run-tests.yml/badge.svg?branch=develop&event=push)](https://github.com/SamuelChorvat/ashcrown-remake/actions/workflows/run-tests.yml)
<br>

Welcome to the backend of Ashcrown, a PvP strategy game. This backend has been rebuilt from the ground up and contains all the game’s logic and mechanics. While the client code is proprietary due to licensing issues, this repo is where the interesting stuff happens. Explore the code and contribute to the evolution of Ashcrown!
## Background on the Remake
I originally built the backend in Java with SmartFoxServer 2X (SFS2X) but opted to rewrite it as a REST API, leveraging client polling for updates. I replaced the traditional account system with simple play sessions, eliminating the need for a database, mail server, and other account-related infrastructure.
<br>
<br>
The client code, still developed in Unity, was originally a standalone app available on the App Store, Google Play, and Steam. Due to the time-consuming nature of managing and testing releases across these platforms, even with deployment pipelines, I decided to convert it to WebGL to be playable directly from the browser. Since the game is not resource-intensive, this approach was feasible.

<br>![ReadmeLogo](ReadmeLogo.png)