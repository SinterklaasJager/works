"use strict";

import {
    sum,
    RemoveDuplicates,
    filter
} from "./script";

test('adds 1 + 2 to equal 3', () => {
    expect(sum(1, 2)).toBe(3);
});

test('functie verwijdert alle duplicaten', () => {
    let arr = ["a", "b", "b", "c", "c", "c", "d", "d", "d", "d"]
    expect(RemoveDuplicates(arr)).toStrictEqual(['a', 'b', 'c', 'd']);
});

test('functie filter op meegegeven parameters, geeft alle objecten terug, waar a:1 in zit', () => {
    let x = {
        "a": 1,
        "b": 2
    }
    let y = {
        "c": 3,
        "d": 4
    }

    let z = {
        "e": 5,
        "a": 1
    }

    let arr = [x, y, z]

    expect(filter(arr, "a", 1)).toEqual(
        expect.not.objectContaining(y),
    );;
});