#!/bin/sh

DIR_DESKTOP=$(xdg-user-dir DESKTOP)
DIR_GAME=$(dirname "$0")

echo "Removing desktop icon..."
rm $DIR_DESKTOP/game-1812-la-aventura.desktop

#echo "Removing icon in game directory..."
#rm $DIR_GAME/game-1812-la-aventura.desktop

echo "Removing launcher menu entry..."
rm ~/.local/share/applications/game-1812-la-aventura.desktop
 
echo "Done."