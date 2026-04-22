#!/usr/bin/env bash

STOWDIR="$HOME/.config/hypr/theme"
STOWTARGET="$HOME"
COREPKG="c.c"
RELOADSCRIPT="$HOME/.config/scripts/reload.sh"

# Формируем список: берем имена папок, исключая COREPKG
# Используем sort для порядка
pkglist=$(ls -1 "$STOWDIR" | grep -v "^$COREPKG$")

# Если список пуст — выходим
if [[ -z "$pkglist" ]]; then
    echo "DEBUG: No themes found in $STOWDIR"
    exit 1
fi

echo "DEBUG: Found themes: $pkglist"

# Выбор через Rofi
# -no-config гарантирует, что мы не берем настройки из других файлов
# -theme прописывает путь к твоему строгому лаймовому конфигу
selection=$(echo "$pkglist" | rofi -dmenu -i -p "Select Theme" \
    -no-config \
    -theme "$HOME/.config/rofi/themes/hyprmed.rasi")

# Если нажали ESC
[[ -z "$selection" ]] && exit 0

echo "Switching to: $selection"

# Переключение
for pkg in $(ls -1 "$STOWDIR" | grep -v "^$COREPKG$"); do
  stow --dir "$STOWDIR" --target "$STOWTARGET" --delete "$pkg" 2>/dev/null
done

stow --dir "$STOWDIR" --target "$STOWTARGET" --stow "$selection"
stow --dir "$STOWDIR" --target "$STOWTARGET" --restow "$COREPKG"

# Перезагрузка
"$RELOADSCRIPT"