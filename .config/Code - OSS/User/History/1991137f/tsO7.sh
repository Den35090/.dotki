#!/usr/bin/env bash
set -x # ВКЛЮЧАЕМ ОТЛАДКУ

STOWDIR="$HOME/.config/hypr/themes"
STOWTARGET="$HOME"
COREPKG="c.c"
RELOADSCRIPT="$HOME/.config/scripts/reload.sh"

selection=$(ls -1 "$STOWDIR" | grep -v "^$COREPKG$" | rofi -dmenu -i -p "Select Theme" -no-config -theme "$HOME/.config/rofi/themes/hyprmed.rasi")

[[ -z "$selection" ]] && exit 0

echo "Switching to: $selection"

# Удаляем старые линки
for pkg in $(ls -1 "$STOWDIR" | grep -v "^$COREPKG$"); do
  stow --dir "$STOWDIR" --target "$STOWTARGET" --delete "$pkg"
done

# Применяем новую
stow --dir "$STOWDIR" --target "$STOWTARGET" --stow "$selection"
stow --dir "$STOWDIR" --target "$STOWTARGET" --restow "$COREPKG"

# Проверка reload.sh
echo "Running reload script..."
bash "$RELOADSCRIPT" 

echo "Done."
set +x