# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.4.0] - 2023-04-27

### Added

- Bean saving. In the custom menu, you can save and load beans.
- An auto mode. Rounds will automatically advance after you start when this option is on.

### Changed

- The menu background now shows the color and opposite color of the previous winner.
- The custom battle menu background matches the main menu background, but is dimmer.
- The camera now uses Cinemachine to switch between positions, making it much smoother and more efficient.
- Beans will now be considered "dead" at Y level -1 instead of 0, opening the oppurtunity for epic comebacks.

### Fixed

- Hats will no longer clip through the background of the match intro. 
- Similarly, the match intro background has been adjusted to fill the entire screen.

## [o.3.o] - 2022-09-28

### Added

- A side bar on the left with the round number, match number, and bean names.
- Camera control has been added. There's a box with allows you to pick a side of the arena to view, and 3 arrows allowing you to pick a vertical angle for the camera. There are also eye icons next to the bean names allowing you to see from the perspective of a bean.

### Changed

- Now using Unity 2022 instead of Unity 2020.
- Most of the UI has been reworked to be easier to use.
- Resolution is no longer locked, 16:9 is recommended.
- Replaced the colored square in the Custom Battle menu with a preview of the bean.
- Buttons are now used instead of pressing enter.

### Fixed

- The platform should no longer start growing after shrinking to nothing in overtime.

## [0.2.0] - 2022-09-11

### Added

- An intro at the start of each match, showing the beans.
- Beans now have small trails.
- In Custom Battle, beans can now be assigned a name so you can fight your friends as beans. (Note: you still can't actually control the beans)

### Changed

- Scripts have been optimized for slightly better performance.

### Fixed

- An error that occured every time a bean dies. This wasn't really a problem but it could've been in the future.

## [0.1.0] - 2022-09-06

### Added

- Ten different hats! Each bean will get a random one. More cosmetics coming in the future.
- Custom Battle mode, where you can pick the colors and cosmetics for each bean.

### Changed

- Menu now has a button to start the game instead of pressing space.

### Fixed

- Victory background will now consistently use the correct color.

## [0.0.1] - 2022-09-04

### Added

- Everything. lol
