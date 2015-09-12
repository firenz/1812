#!/bin/sh
#
# Simple XDG Install Script for Linux Games
#
# Features:
# - Communicates with the desktop via the xdg-utils vendor integration scripts.
#   (No need to upgrade if the implementation details change.)
# - Icons are resolved via the desktop theming system, granting theme
#   developers the ability to provide customized versions which preserve the
#   overall system aesthetic.
# - Uses the "TryExec" feature to prevent dead launcher entries by hiding the
#   entry automatically if the file it points to is deleted or non-executable.
# - Tested against bash, dash, ksh93, pdksh, zsh, and BusyBox shell for
#   portability.
# - Can be re-run to update existing installs as long as GAME_ID remains
#   unchanged.
# - Clean error reporting if the user is running an esoteric distro which
#   doesn't install xdg-utils by default.
# - Can generate a wrapper script for you if you're doing something which
#   expects a certain working directory (eg. relative-path .so loading)
#
# Instructions for reuse:
#
# 1. Choose an identifier for your game which consists of an alphabetical
#    vendor prefix, a dash, and a name for your game containing no spaces and
#    place it in GAME_ID. NEVER CHANGE THIS!
# 2. Change GAME_NAME to the human-readable name for your game.
# 3. Write a descriptive synopsis and place it in GAME_SYNOPSIS to be displayed
#    by the user's launcher (usually as a tooltip).
# 4. Set GAME_EXEC to the path to the launch script, relative to this file.
# 5. Set ICON_SIZE to the size of the icon in pixels as an integer.
#    (Size of one edge. Square icons are assumed.)
# 6. Select a category (usually "Game") and associated subcategories from
#    http://standards.freedesktop.org/menu-spec/latest/apa.html and
#    http://standards.freedesktop.org/menu-spec/latest/apas02.html
#    and place them in CATEGORIES in the form of a semicolon-separated list
#    with no spaces and a terminal semicolon.
# 7. Provide a PNG-format icon at the specified size and adjust ICON_PATH
#    to point to it.
# 8. If the game requires the working directory to be set to the executable's
#    parent directory, set NEEDS_WRAPPER to 1.
#
# Development Resources:
# - http://standards.freedesktop.org/desktop-entry-spec/latest/
# - http://standards.freedesktop.org/icon-theme-spec/latest/
# - http://standards.freedesktop.org/menu-spec/latest/
#
# The newest version of this script can be found at:
#  https://gist.github.com/ssokolow/7010485
#
# Copyright (c) 2013, 2014 Stephan Sokolow
#
# Permission is hereby granted, free of charge, to any person obtaining a copy
# of this software and associated documentation files (the "Software"), to deal
# in the Software without restriction, including without limitation the rights
# to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
# copies of the Software, and to permit persons to whom the Software is
# furnished to do so, subject to the following conditions:
#
# The above copyright notice and this permission notice shall be included in
# all copies or substantial portions of the Software.
#
# THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
# IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
# FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
# AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
# LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
# OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
# THE SOFTWARE.
#
 
# --== Per-Game Configuration ==--
 
GAME_ID="game-1812-la-aventura"
GAME_NAME="1812: La Aventura"
GAME_SYNOPSIS="Demostración de sistema de aventura gráfica desarrollada en Unity2D"
GAME_EXEC="run_1812.sh"
ICON_PATH="1812_aventura_linux_x86_Data/icon.png"
ICON_SIZE=256
CATEGORIES="Game;AdventureGame;"
NEEDS_WRAPPER=0
 
# --== Helper Functions ==--
 
# Shorthand for checking if a command is available
is_installed() { type "$1" 1>/dev/null 2>&1; return $?; }
 
# A robust way to display a message to the user
robust_msg() {
    if is_installed kdialog; then
        if [ "$1" = "error" ]; then
            kdialog --title "$2" "--$1" "$3"
        elif [ "$1" = "warning" ]; then
            kdialog --title "$2" --sorry "$3"
        else
            kdialog --title "$2" --msgbox "$3"
        fi
    elif is_installed zenity; then
        zenity "--$1" --title "$2" --no-markup --text "$3"
    elif is_installed xmessage; then
        xmessage "$3"
    fi
 
    # Always print to stderr in case no GUI is running
    LABEL=`echo "$1" | tr "a-z" "A-Z"`
    printf "$LABEL: $3\n\n" 1>&2
}
 
# Shorthand for checking for required commands and displaying a message
require_cmd() {
    if ! is_installed "$1"; then
        TITLE="Missing Dependency"
        MSG="Could not find command:\n\n%s\n"
        MSG="$MSG\nThis command should have been installed by default and "
        MSG="$MSG\nshould be available in your package manager via a package"
        MSG="$MSG\nwith a name like '%s'."
        MSG=`printf "$MSG" "$1" "$2"`
 
        robust_msg error "$TITLE" "$MSG"
        exit 1
    fi
}
 
# Shorthand for checking for required package files and displaying a message
require_file() {
    if ! [ -f "$1" ]; then
        MSG="The install script could not find the $2 at:\n"
        MSG="$MSG\n    ./$1\n"
        MSG="$MSG\nInstallation will abort."
        robust_msg error "Failure" "$MSG"
        exit 2
    fi
}
 
# --== Main Script Body ==--
 
# Verify the presence of the xdg-utils vendor scripts
require_cmd xdg-icon-resource xdg-utils
require_cmd xdg-desktop-menu xdg-utils
require_cmd xdg-desktop-icon xdg-utils
 
# Make sure the game binary and icon are actually present
require_file "$GAME_EXEC" game
require_file "$ICON_PATH" icon
 
# Make sure the game binary is executable
# (eg. if the archive didn't preserve POSIX permissions)
chmod +x "$GAME_EXEC"
 
# Prepare a temporary directory
TEMPDIR="`mktemp -d`"
LAUNCHER="$TEMPDIR/$GAME_ID.desktop"
 
# Install the icon into the desktop's fallback theme
echo "Installing ${ICON_SIZE}x${ICON_SIZE}px icon into system theme..."
xdg-icon-resource install --size "$ICON_SIZE" "$ICON_PATH" "$GAME_ID"
 
# Some desktops don't recognize extra-large icons so, if we have one and the
# near-ubiquitous ImageMagick is available, generate a 256x256px icon.
if [ "$ICON_SIZE" -gt 256 ]; then
    if is_installed convert; then
        echo 'Creating and installing 256x256px icon for best compatibility...'
        convert "$ICON_PATH" -resize 256x256 "$TEMPDIR/256.png"
        xdg-icon-resource install --size 256 "$TEMPDIR/256.png" "$GAME_ID"
    else
        MSG="WARNING: Could not find the ImageMagic 'convert' command.\n"
        MSG="$MSG\nThis program provides an extra large icon that some "
        MSG="$MSG\ndesktops may have difficulty displaying. If you experience"
        MSG="$MSG\nproblems, please install ImageMagick and rerun this script"
        MSG="$MSG\nto generate a smaller copy for maximum compatibility."
        MSG=`printf "$MSG"`
        robust_msg warning "Potential Problem" "$MSG"
    fi
fi
 
 
# Write a wrapper script, if necessary
if [ "$NEEDS_WRAPPER" ] && [ "$NEEDS_WRAPPER" -ge "1" ]; then
    echo "Creating launcher script..."
 
    # Escape a relative path so the wrapper will work if the folder moves
    EXEC_PATH=`echo "./$GAME_EXEC" | sed 's/\(["\`$\\]\)/\\\1/g'`
 
    cat << EOF > play.sh
#!/bin/sh
cd "\`dirname \\"\$0\\"\`"
exec $EXEC_PATH
EOF
    chmod +x play.sh
 
    # Reset EXEC_PATH to the wrapper script so the launcher will use it
    EXEC_PATH=`echo "$PWD/play.sh" | sed 's/\(["\`$\\]\)/\\\1/g'`
else
    # Otherwise, just use an escaped path to the game directly.
    EXEC_PATH=`echo "$PWD/$GAME_EXEC" | sed 's/\(["\`$\\]\)/\\\1/g'`
fi
 
# Create a launcher definition for the game with the correct paths
cat << EOF > "$LAUNCHER"
[Desktop Entry]
Encoding=UTF-8
Name=$GAME_NAME
Comment=$GAME_SYNOPSIS
Type=Application
Exec="$EXEC_PATH"
TryExec=$EXEC_PATH
Icon=$GAME_ID
Categories=$CATEGORIES
EOF
 
# Install the launcher into the menu
echo "Installing launcher menu entry..."
xdg-desktop-menu install "$LAUNCHER"
 
# Add a desktop icon
echo "Installing desktop icon..."
xdg-desktop-icon install "$LAUNCHER"

# Add a launcher icon in game directory
#dir=$(PWD)
#echo "$dir"
#echo "Installing icon in game directory..."
#xdg-desktop-icon install "$dir" "$LAUNCHER"
 
# Clean up after ourselves
rm -rf "$TEMPDIR"
 
echo "Done."

#Executing game
echo "Executing game..."

cd "$(dirname "$0")"
 
exec ./1812_aventura_linux_x86.x86 "$@"