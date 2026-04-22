#!/usr/bin/env bash
set -x # ВКЛЮЧАЕМ ОТЛАДКУ

STOWDIR="$HOME/themes"
STOWTARGET="$HOME/.config"
COREPKG="c.c"
RELOADSCRIPT="$HOME/.config/scripts/reload.sh"

# Выбор темы через Rofi
selection=$(ls -1 "$STOWDIR" | grep -v "^$COREPKG$" | rofi -dmenu -i -p "Select Theme" -no-config -theme "$HOME/.config/rofi/themes/hyprmed.rasi")

[[ -z "$selection" ]] && exit 0

echo "Switching to: $selection"

# 1. Удаляем старые линки
# Проходимся по всем темам и удаляем их симлинки из ~/.config/
for pkg in $(ls -1 "$STOWDIR" | grep -v "^$COREPKG$"); do
  stow --dir "$STOWDIR/$pkg/.config" --target "$STOWTARGET" --delete . 2>/dev/null
done

# 2. Применяем новую тему
# Указываем stow работать с содержимым папки .config внутри выбранной темы
stow --dir "$STOWDIR/$selection/.config" --target "$STOWTARGET" --stow .

# 3. Применяем ядро (COREPKG)
stow --dir "$STOWDIR/$COREPKG/.config" --target "$STOWTARGET" --restow .

# 4. Проверка и запуск скрипта перезагрузки
if [[ -f "$RELOADSCRIPT" ]]; then
    echo "Running reload script..."
    bash "$RELOADSCRIPT"
else
    echo "Error: reload.sh not found at $RELOADSCRIPT"
fi

echo "Done."
set +x